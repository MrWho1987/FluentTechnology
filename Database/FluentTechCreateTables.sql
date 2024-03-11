CREATE DATABASE FluentTechDB;

\c FluentTechDB;

-- Ensure the UUID extension is available in your PostgreSQL instance
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Preferred Communication Methods Table
CREATE TABLE preferred_communication_methods (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    method VARCHAR(255) NOT NULL UNIQUE
);
COMMENT ON TABLE preferred_communication_methods IS 'Stores preferred communication methods (e.g., email, SMS).';

-- Organization Types Table
CREATE TABLE organization_types (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    type_name VARCHAR(255) NOT NULL UNIQUE
);
COMMENT ON TABLE organization_types IS 'Stores types of organizations (e.g., non-profit, educational institution).';

-- Grant Categories Table
CREATE TABLE grant_categories (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    category_name VARCHAR(255) NOT NULL UNIQUE
);
COMMENT ON TABLE grant_categories IS 'Stores grant categories of interest (e.g., Education, Health).';

-- Personalized Content Preferences Table
CREATE TABLE personalized_content_preferences (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    preference_name VARCHAR(255) NOT NULL UNIQUE
);
COMMENT ON TABLE personalized_content_preferences IS 'Stores personalized content preferences (e.g., News and Updates, Success Stories).';

-- Users Table
CREATE TABLE users (
    email_address VARCHAR(255) PRIMARY KEY,
    full_name VARCHAR(255) NOT NULL,
    preferred_communication_method_id UUID NOT NULL,
    organization_type_id UUID NOT NULL,
    FOREIGN KEY (preferred_communication_method_id) REFERENCES preferred_communication_methods(id),
    FOREIGN KEY (organization_type_id) REFERENCES organization_types(id)
);
COMMENT ON TABLE users IS 'Stores user information including their preferred communication method and organization type.';

-- User Grant Categories Junction Table
CREATE TABLE user_grant_categories (
    email_address VARCHAR(255),
    grant_category_id UUID,
    PRIMARY KEY (email_address, grant_category_id),
    FOREIGN KEY (email_address) REFERENCES users(email_address) ON DELETE CASCADE,
    FOREIGN KEY (grant_category_id) REFERENCES grant_categories(id) ON DELETE CASCADE
);
COMMENT ON TABLE user_grant_categories IS 'Links users to their grant categories of interest, supporting multiple interests per user.';

-- User Personalized Content Preferences Junction Table
CREATE TABLE user_personalized_content_preferences (
    email_address VARCHAR(255),
    preference_id UUID,
    PRIMARY KEY (email_address, preference_id),
    FOREIGN KEY (email_address) REFERENCES users(email_address) ON DELETE CASCADE,
    FOREIGN KEY (preference_id) REFERENCES personalized_content_preferences(id) ON DELETE CASCADE
);
COMMENT ON TABLE user_personalized_content_preferences IS 'Links users to their personalized content preferences, allowing multiple preferences per user.';

-- Indexes for Foreign Keys in Users Table
CREATE INDEX idx_users_preferred_communication_method_id ON users(preferred_communication_method_id);
CREATE INDEX idx_users_organization_type_id ON users(organization_type_id);

-- Indexes for Junction Tables
-- These indexes will improve the performance of queries joining these tables and looking up values.
CREATE INDEX idx_user_grant_categories_email_address ON user_grant_categories(email_address);
CREATE INDEX idx_user_grant_categories_grant_category_id ON user_grant_categories(grant_category_id);

CREATE INDEX idx_user_personalized_content_preferences_email_address ON user_personalized_content_preferences(email_address);
CREATE INDEX idx_user_personalized_content_preferences_preference_id ON user_personalized_content_preferences(preference_id);
