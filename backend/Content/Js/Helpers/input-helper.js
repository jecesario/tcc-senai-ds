const inputEntries = [
    ...document.querySelectorAll(
      ".form-container > form > .input-container > input, .form-container > form > .input-container > select"
    ),
  ];
console.log(inputEntries)
  const labelEntries = [
    ...document.querySelectorAll(
      ".form-container > form > .input-container > label"
    ),
  ];

  const inputFields = inputEntries.map((inputEntry) => {
    return {
      id: inputEntry.id,
      value: inputEntry.value,
      focus: function (label) {
        inputEntry.addEventListener("focus", function(e) {
          e.preventDefault();
          if(inputEntry.id === label.htmlFor){
            label.classList.add("focus-input");
          }
        });
      },
      blur: function (label) {
        inputEntry.addEventListener("blur", function(e) {
          e.preventDefault();
          if (!inputEntry.value && inputEntry.id === label.htmlFor) {
            label.classList.remove("focus-input");
          }
        });
      },
    };
  });

  const loadEvents = (input) => {
    labelEntries.forEach(label => {
        input.focus(label);
        input.blur(label);
    })
  }

  inputFields.forEach(input => {
      loadEvents(input);
  });