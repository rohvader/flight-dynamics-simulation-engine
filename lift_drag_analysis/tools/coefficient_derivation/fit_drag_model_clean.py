from __future__ import annotations

import argparse
import json
from pathlib import Path

import numpy as np


def load_points(path: Path) -> tuple[np.ndarray, np.ndarray]:
    with path.open("r", encoding="utf-8") as file:
        payload = json.load(file)

    points = payload["points"]
    alpha = np.array([point["alpha_deg"] for point in points], dtype=float)
    cd = np.array([point["cd"] for point in points], dtype=float)
    return alpha, cd


def fit_drag_polynomial(alpha: np.ndarray, cd: np.ndarray, degree: int) -> np.ndarray:
    return np.polyfit(alpha, cd, degree)


def evaluate_polynomial(coefficients: np.ndarray, alpha: np.ndarray) -> np.ndarray:
    return np.polyval(coefficients, alpha)


def rmse(actual: np.ndarray, predicted: np.ndarray) -> float:
    return float(np.sqrt(np.mean((actual - predicted) ** 2)))


def coefficients_to_horner_string(coefficients: np.ndarray) -> str:
    terms = [f"{coefficient:.10g}" for coefficient in coefficients]
    expression = terms[0]

    for coefficient in terms[1:]:
        expression = f"({expression} * alpha + {coefficient})"

    return expression


def main() -> None:
    parser = argparse.ArgumentParser(description="Fit a polynomial drag coefficient model from JSON data")
    parser.add_argument("--input", type=Path, default=Path("data/raw/drag_coefficient_points.json"))
    parser.add_argument("--output", type=Path, default=Path("data/processed/drag_model_coefficients.json"))
    parser.add_argument("--degree", type=int, default=2)
    args = parser.parse_args()

    alpha, cd = load_points(args.input)
    coefficients = fit_drag_polynomial(alpha, cd, args.degree)
    predictions = evaluate_polynomial(coefficients, alpha)

    output = {
        "metadata": {
            "model": "Polynomial fit for Cd(alpha)",
            "degree": args.degree,
            "alpha_units": "degrees",
            "coefficient_order": "highest_power_first",
            "rmse": rmse(cd, predictions),
            "horner_expression": coefficients_to_horner_string(coefficients),
        },
        "coefficients": [float(value) for value in coefficients],
    }

    args.output.parent.mkdir(parents=True, exist_ok=True)
    with args.output.open("w", encoding="utf-8") as file:
        json.dump(output, file, indent=2)

    print(json.dumps(output, indent=2))


if __name__ == "__main__":
    main()
