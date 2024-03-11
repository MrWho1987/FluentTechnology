import React from 'react';

const Dropdown = ({ label, options, onChange, name, value }) => (
    <div>
      <label>{label}</label>
      <select name={name} value={value} onChange={onChange}>
        {options.map((option) => (
          <option key={option.id} value={option.id}>
            {option.name}
          </option>
        ))}
      </select>
    </div>
  );
  

export default Dropdown;
