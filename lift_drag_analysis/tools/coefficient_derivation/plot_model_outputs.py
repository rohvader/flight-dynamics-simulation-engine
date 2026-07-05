from __future__ import annotations

import argparse
import json
from pathlib import Path

import matplotlib.pyplot as plt
import numpy as np


def load_json(path: Path) -> dict:
    with path.open("r", encoding="utf-8") as file:
        return json.load(file)


def plot_drag(input_path: Path, output_dir: Path) -> None:
    payload = load_json(input_path)
    points = payload["points"]

    alpha = np.array([point["alpha_deg"] for point in points], dtype=float)
    cd = np.array([point["cd"] for point in points], dtype=float)

    coefficients = np.polyfit(alpha, cd, 2)
    fitted_alpha = np.linspace(float(alpha.min()), float(alpha.max()), 300)
    fitted_cd = np.polyval(coefficients, fitted_alpha)

    plt.figure()
    plt.scatter(alpha, cd, label="Data points")
    plt.plot(fitted_alpha, fitted_cd, label="Quadratic fit")
    plt.xlabel("Angle of attack / degrees")
    plt.ylabel("Drag coefficient")
    plt.title("Drag coefficient fit")
    plt.legend()
    plt.grid(True)
    plt.tight_layout()
    plt.savefig(output_dir / "drag_coefficient_fit.png", dpi=200)
    plt.close()


def plot_lift(input_path: Path, output_dir: Path) -> None:
    payload = load_json(input_path)
    points = payload["points"]

    plt.figure()

    grouped: dict[float, list[dict]] = {}
    for point in points:
        grouped.setdefault(float(point["reynolds_number"]), []).append(point)

    for reynolds, group in sorted(grouped.items()):
        group = sorted(group, key=lambda item: item["alpha_deg"])
        alpha = [point["alpha_deg"] for point in group]
        cl = [point["cl"] for point in group]
        plt.plot(alpha, cl, label=f"Re = {reynolds:.1e}")

    plt.xlabel("Angle of attack / degrees")
    plt.ylabel("Lift coefficient")
    plt.title("Lift coefficient curves")
    plt.legend()
    plt.grid(True)
    plt.tight_layout()
    plt.savefig(output_dir / "lift_reynolds_curves.png", dpi=200)
    plt.close()


def main() -> None:
    parser = argparse.ArgumentParser(description="Plot coefficient data")
    parser.add_argument("--output-dir", type=Path, default=Path("data/plots"))
    args = parser.parse_args()

    args.output_dir.mkdir(parents=True, exist_ok=True)

    plot_drag(Path("data/raw/drag_coefficient_points.json"), args.output_dir)
    plot_lift(Path("data/raw/lift_reynolds_points.json"), args.output_dir)

    print(f"Wrote plots to {args.output_dir}")


if __name__ == "__main__":
    main()
