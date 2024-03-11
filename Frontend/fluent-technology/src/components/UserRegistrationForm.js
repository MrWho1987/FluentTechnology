import React, { useState, useEffect } from 'react';
import Dropdown from './Dropdown';
import CheckboxGroup from './CheckboxGroup';
import { fetchDropdownData } from '../services/apiService';
import axios from 'axios';

const UserRegistrationForm = () => {
    const [formData, setFormData] = useState({
        fullName: '',
        emailAddress: '',
        preferredCommunicationMethodId: '',
        organizationTypeId: '',
        customPreferredCommunicationMethod: '',
        customOrganizationTypeName: '',
        grantCategoryIds: [],
        personalizedContentPreferenceIds: [],
    });

    const [dropdownData, setDropdownData] = useState({
        communicationMethods: [],
        organizationTypes: [],
        grantCategories: [],
        contentPreferences: [],
    });

    useEffect(() => {
        const loadData = async () => {
            try {
                const data = await fetchDropdownData();
                setDropdownData(data);
            } catch (error) {
                console.error("Error fetching dropdown data:", error);
                alert("Failed to load form data.");
            }
        };
        loadData();
    }, []);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        let updates = { [name]: value };

        // Reset custom fields if 'Other' is not selected
        if (name === 'preferredCommunicationMethodId' && value !== 'other') {
            updates.customPreferredCommunicationMethod = '';
        }
        if (name === 'organizationTypeId' && value !== 'other') {
            updates.customOrganizationTypeName = '';
        }

        setFormData({ ...formData, ...updates });
    };

    const handleCheckboxChange = (e) => {
        const { checked, value, name } = e.target;
        setFormData(prevFormData => {
            const updatedArray = checked
                ? [...prevFormData[name], value]
                : prevFormData[name].filter(item => item !== value);

            return { ...prevFormData, [name]: updatedArray };
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        // Construct payload based on the selected options and custom inputs
        const payload = {
            fullName: formData.fullName,
            emailAddress: formData.emailAddress,
            preferredCommunicationMethodId: formData.preferredCommunicationMethodId !== 'other' ? formData.preferredCommunicationMethodId : undefined,
            organizationTypeId: formData.organizationTypeId !== 'other' ? formData.organizationTypeId : undefined,
            customPreferredCommunicationMethod: formData.preferredCommunicationMethodId === 'other' ? formData.customPreferredCommunicationMethod : undefined,
            customOrganizationTypeName: formData.organizationTypeId === 'other' ? formData.customOrganizationTypeName : undefined,
            grantCategoryIds: formData.grantCategoryIds,
            personalizedContentPreferenceIds: formData.personalizedContentPreferenceIds,
        };

        try {
            const response = await axios.post('https://localhost:7096/api/UserRegistration/register', payload);
            console.log('Registration success:', response.data);
            alert('User registered successfully!');
            // Reset the form or navigate the user as needed
        } catch (error) {
            console.error('Failed to register user:', error);
            alert('Failed to register user. Please try again.');
        }
    };


    return (
        <form onSubmit={handleSubmit}>
            <input
                name="fullName"
                value={formData.fullName}
                onChange={handleInputChange}
                placeholder="Full Name"
                required
            />
            <input
                name="emailAddress"
                type="email"
                value={formData.emailAddress}
                onChange={handleInputChange}
                placeholder="Email Address"
                required
            />
            <Dropdown
                label="Preferred Communication Method"
                options={[...dropdownData.communicationMethods, { id: 'other', name: 'Other' }]}
                value={formData.preferredCommunicationMethodId}
                onChange={handleInputChange}
                name="preferredCommunicationMethodId" // Make sure this name matches the state property
            />

            {formData.preferredCommunicationMethodId === 'other' && (
                <input
                    name="customPreferredCommunicationMethod"
                    value={formData.customPreferredCommunicationMethod}
                    onChange={handleInputChange}
                    placeholder="Specify Preferred Communication Method"
                    required
                />
            )}

            <Dropdown
                label="Organization Type"
                options={[...dropdownData.organizationTypes, { id: 'other', name: 'Other' }]}
                value={formData.organizationTypeId}
                onChange={handleInputChange}
                name="organizationTypeId" // Make sure this name matches the state property
            />

            {formData.organizationTypeId === 'other' && (
                <input
                    name="customOrganizationTypeName"
                    value={formData.customOrganizationTypeName}
                    onChange={handleInputChange}
                    placeholder="Specify Organization Type"
                    required
                />
            )}
            <CheckboxGroup
                label="Grant Categories of Interest"
                options={dropdownData.grantCategories}
                name="grantCategoryIds"
                selectedOptions={formData.grantCategoryIds}
                onChange={handleCheckboxChange}
            />
            <CheckboxGroup
                label="Personalized Content Preferences"
                options={dropdownData.contentPreferences}
                name="personalizedContentPreferenceIds"
                selectedOptions={formData.personalizedContentPreferenceIds}
                onChange={handleCheckboxChange}
            />
            <button type="submit">Register</button>
        </form>
    );
};

export default UserRegistrationForm;
