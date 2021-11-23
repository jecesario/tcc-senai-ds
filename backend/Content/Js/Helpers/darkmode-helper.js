const mode = document.querySelector(".switch-mode-container input");
const head = document.getElementsByTagName("HEAD")[0];
const link = document.createElement("link");
link.rel = "stylesheet";
link.type = "text/css";
link.href = "/Content/Css/darkmode.css";

mode.addEventListener("change", (event) => {
  event.preventDefault();
  localStorage.setItem("darkmodeActive", mode.checked);
  if (mode.checked) {
    head.appendChild(link);
    return;
  }

  head.removeChild(link);
});

const loadDarkmode = () => {
    const darkmodeActive = localStorage.getItem("darkmodeActive") === 'true';
    if (darkmodeActive) {
      head.appendChild(link);
      mode.checked = true;
    }
  };
  
window.addEventListener("DOMContentLoaded", (event) => {
loadDarkmode();
});
  