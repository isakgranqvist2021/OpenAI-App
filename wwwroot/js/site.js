window.onload = () => {
  const timestamps = document.querySelectorAll(".timestamp");

  timestamps.forEach((element) => {
    const d = new Date(0);
    d.setSeconds(parseInt(element.textContent));

    const intl = Intl.DateTimeFormat("en", {
      hour: "numeric",
      minute: "numeric",
      day: "numeric",
      month: "short",
      year: "numeric",
    });

    element.textContent = intl.format(d);
  });
};
