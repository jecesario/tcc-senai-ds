const inputEntries = [
    ...document.querySelectorAll(
      ".input-container > input, .input-container > select, .input-container > textarea"
    ),
  ];
  const labelEntries = [
    ...document.querySelectorAll(
      ".input-container > label"
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
        onload: function (label) {
            window.addEventListener("load", function (e) {
                e.preventDefault();
                if (inputEntry.value) {
                    label.classList.add("focus-input");
                }
            });
        }
    };
  });

  const loadEvents = (input) => {
    labelEntries.forEach(label => {
        input.focus(label);
        input.blur(label);
        input.onload(label);
    })
  }

  inputFields.forEach(input => {
      loadEvents(input);
  });