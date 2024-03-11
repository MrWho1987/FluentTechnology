const CheckboxGroup = ({ label, options, name, onChange }) => (
    <div>
      <p>{label}</p>
      {options.map(option => (
        <label key={option.id}>
          <input
            type="checkbox"
            value={option.id}
            name={name}
            onChange={onChange} // Directly use onChange without wrapping
          />
          {option.name}
        </label>
      ))}
    </div>
  );
  export default CheckboxGroup;

  