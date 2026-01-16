// src/App.test.tsx
import { describe, it, expect } from "vitest";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import App from "./App";

describe("App", () => {
  it("renders the Vite + React heading and initial count", () => {
    render(<App />);

    expect(
      screen.getByRole("heading", { name: /vite \+ react/i }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("button", { name: /count is 0/i }),
    ).toBeInTheDocument();
    expect(screen.getByText(/edit/i)).toBeInTheDocument();
  });

  it("increments count when button is clicked", async () => {
    const user = userEvent.setup();
    render(<App />);

    const button = screen.getByRole("button", { name: /count is 0/i });
    await user.click(button);

    expect(
      screen.getByRole("button", { name: /count is 1/i }),
    ).toBeInTheDocument();

    await user.click(screen.getByRole("button", { name: /count is 1/i }));
    expect(
      screen.getByRole("button", { name: /count is 2/i }),
    ).toBeInTheDocument();
  });

  it("renders both logo links", () => {
    render(<App />);

    const links = screen.getAllByRole("link");
    const hrefs = links.map((a) => a.getAttribute("href"));

    expect(hrefs).toEqual(
      expect.arrayContaining(["https://vite.dev", "https://react.dev"]),
    );
  });
});
