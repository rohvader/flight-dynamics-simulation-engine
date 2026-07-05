from __future__ import annotations

import json
import subprocess
import sys
from pathlib import Path


def run_script(script: str) -> None:
    subprocess.run([sys.executable, script], check=True)


def main() -> None:
    run_script("tools/coefficient_derivation/fit_drag_model.py")
    run_script("tools/coefficient_derivation/fit_lift_reynolds_model.py")

    drag_path = Path("data/processed/drag_model_coefficients.json")
    lift_path = Path("data/processed/lift_model_coefficients.json")

    with drag_path.open("r", encoding="utf-8") as file:
        drag = json.load(file)

    with lift_path.open("r", encoding="utf-8") as file:
        lift = json.load(file)

    combined = {
        "metadata": {
            "description": "Combined aerodynamic coefficient output for Unity/C# runtime use",
        },
        "drag": drag,
        "lift": lift,
    }

    output_path = Path("data/processed/aero_model_coefficients.json")
    with output_path.open("w", encoding="utf-8") as file:
        json.dump(combined, file, indent=2)

    print(f"Wrote {output_path}")


if __name__ == "__main__":
    main()
