import React from "react";

const SplitTypeInput = ({ handleValueChange, index, placeholder }) => {
  return (
    <input
      type="number"
      min={0}
      step={0.01}
      placeholder={placeholder}
      onChange={(e) =>
        handleValueChange(index, parseFloat(e.target.value) || 0)
      }
    ></input>
  );
};

export default SplitTypeInput;
