import axios from 'axios';

const BASE_URL = 'https://localhost:7096/api/Lookup'; // Use http if your local setup does not support https

export const fetchDropdownData = async () => {
  try {
    const [communicationMethods, organizationTypes, grantCategories, contentPreferences] = await Promise.all([
      axios.get(`${BASE_URL}/preferred-communication-methods`),
      axios.get(`${BASE_URL}/organization-types`),
      axios.get(`${BASE_URL}/grant-categories`),
      axios.get(`${BASE_URL}/personalized-content-preferences`),
    ]);

    return {
      communicationMethods: communicationMethods.data,
      organizationTypes: organizationTypes.data,
      grantCategories: grantCategories.data,
      contentPreferences: contentPreferences.data,
    };
  } catch (error) {
    console.error("Error fetching dropdown data:", error);
    throw error;
  }
};
