from __future__ import annotations

import argparse
import json
from pathlib import Path

import numpy as np


def load_points(path: Path) -> tuple[np.ndarray, np.ndarray, np.ndarray]:
    with path.open("r", encoding="utf-8") as file:
        payload = json.load(file)

    points = payload["points"]
    alpha = np.array([point["alpha_deg"] for point in points], dtype=float)
    reynolds = np.array([point["reynolds_number"] for point in points], dtype=float)
    cl = np.array([point["cl"] for point in points], dtype=float)
    return alpha, reynolds, cl


def design_matrix(alpha: np.ndarray, reynolds: np.ndarray, baseline_re: float) -> np.ndarray:
    reynolds_term = np.log(reynolds / baseline_re)
    return np.column_stack(
        [
            np.ones_like(alpha),
            alpha,
            reynolds_term,
            alpha * reynolds_term,
        ]
    )


def fit_lift_model(alpha: np.ndarray, reynolds: np.ndarray, cl: np.ndarray, baseline_re: float) -> dict[str, float]:
    x = design_matrix(alpha, reynolds, baseline_re)
    coefficients, *_ = np.linalg.lstsq(x, cl, rcond=None)

    predicted = x @ coefficients
    error = float(np.sqrt(np.mean((cl - predicted) ** 2)))

    return {
        "cl0": float(coefficients[0]),
        "cl_alpha_per_degree": float(coefficients[1]),
        "cl_re_log": float(coefficients[2]),
        "cl_alpha_re_log": float(coefficients[3]),
        "baseline_reynolds_number": float(baseline_re),
        "rmse": error,
    }


def main() -> None:
    parser = argparse.ArgumentParser(description="Fit a Reynolds-corrected lift coefficient model from JSON data")
    parser.add_argument("--input", type=Path, default=Path("data/raw/lift_reynolds_points.json"))
    parser.add_argument("--output", type=Path, default=Path("data/processed/lift_model_coefficients.json"))
    parser.add_argument("--baseline-re", type=float, default=3.4e6)
    args = parser.parse_args()

    alpha, reynolds, cl = load_points(args.input)
    model = fit_lift_model(alpha, reynolds, cl, args.baseline_re)

    output = {
        "metadata": {
            "model": "CL = cl0 + cl_alpha*alpha + cl_re*ln(Re/Re_base) + cl_alpha_re*alpha*ln(Re/Re_base)",
            "alpha_units": "degrees",
        },
        "coefficients": model,
    }

    args.output.parent.mkdir(parents=True, exist_ok=True)
    with args.output.open("w", encoding="utf-8") as file:
        json.dump(output, file, indent=2)

    print(json.dumps(output, indent=2))


if __name__ == "__main__":
    main()
