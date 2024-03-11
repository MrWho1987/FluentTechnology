import React from 'react';
import { createRoot } from 'react-dom/client'; // Updated import to use createRoot
import App from './App';

// Use createRoot for React 18
const container = document.getElementById('root');
const root = createRoot(container); // Create a root instance

// root.render(
//   <App />
// );


root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
