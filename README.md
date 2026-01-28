# Team-Horizon

## Course: CSE-325 
### Team-Horizon Members

1: Ethem Deli
2: Mickael Randriamihaja
3: Andrew Halisky
4: Tafadzwa Chingore
5: Dominic Abah
6: Tsitsi Mutsvedu

## W03 Group Project Proposal
### Project Title : Family Budget & Expense Tracker

### Project Overview

The Family Budget & Expense Tracker is a web-based application designed to help individuals and families manage their personal finances more effectively. Many households struggle to keep track of expenses, stay within a budget, and understand where their money is being spent each month. This application provides a simple, structured way to record expenses, categorize spending, and view monthly summaries.

The application will allow users to create an account, log in securely, and manage their own financial data. Users can add, edit, and delete expenses, assign them to categories, and view summarized reports of their spending. Visual feedback such as charts and totals will help users quickly understand their financial habits and make informed decisions.

This project is valuable because it solves a real-world problem that affects many people. It also provides strong learning opportunities for the team by incorporating core .NET concepts such as MVC/Blazor architecture, database interaction, user authentication, and CRUD operations. The scope is realistic for a semester-long group project while still being meaningful and portfolio-worthy.

### Problem Statement & Target Users
Problem Being Solved:
Many families and individuals lack an easy-to-use system for tracking monthly expenses and budgets. Manual tracking or spreadsheets can be inconsistent, time-consuming, and difficult to maintain.

### Target Users:
Families managing household budgets
Individuals tracking personal expenses
Students learning basic financial management

### Project Scope
What’s IN Scope
User authentication (login and registration)
Expense CRUD (Create, Read, Update, Delete)
Expense categories (food, rent, utilities, etc.)
Monthly expense summaries
Basic charts or visual summaries
Responsive design for desktop and mobile use
❌ What’s OUT of Scope
Bank account integrations
Real-time financial data feeds
Advanced analytics or AI predictions
Multi-currency support
Payment processing

## The focus is on building a stable, well-designed core application rather than adding unnecessary complexity.

### Core App Features (User Actions)
Users can create an account and log in securely
Users can add new expenses
Users can edit or delete existing expenses
Users can assign expenses to categories
Users can view monthly spending summaries
Users can see simple charts showing spending distribution
Users can manage their own data securely

### Example User Story:
As a user, I want to view my monthly expenses by category so I can understand where most of my money is being spent.

Technical Considerations
Data Storage
User profiles
Expense records
Expense categories
Monthly budget data
User Accounts
Yes — authentication required
Each user accesses only their own data
External Services
None required for initial version
Device Compatibility
Desktop
Tablet
Mobile (responsive design)
Basic Security
ASP.NET authentication

Authorization checks
Validation of user input
Secure database access



# Family Budget & Expense Tracker

## PROJECT SETUP & PLANNING
### Card: Project Repository Setup
Description:Create the GitHub repository and add all team members as collaborators.

### Card: Trello Board Setup
Description:Create Trello board, lists (Backlog, To Do, In Progress, Review, Done), and add initial feature cards.

### Card: Define Project Scope
Description:Document what features are in scope and out of scope to prevent scope creep.

### Card: Assign Team Roles
Description:Assign responsibilities such as frontend, backend, database, testing, and documentation.

## USER AUTHENTICATION
### Card: User Registration
Description:Allow users to create an account with validation and secure password storage.

### Card: User Login
Description:Allow registered users to log in securely.

### Card: User Logout
Description:Enable users to log out and end their session.

### Card: Authorization Rules
Description:Ensure users can only access their own data.

## DATABASE & MODELS
### Card: Database Design
Description:Design database tables for users, expenses, categories, and budgets.

### Card: Expense Model
Description:Create model for expenses (amount, date, category, description).

### Card: Category Model
Description:Create model for expense categories.

### Card: Database Migrations
Description:Set up Entity Framework migrations and apply them to the database.

## EXPENSE MANAGEMENT (CRUD)
### Card: Create Expense
Description:Allow users to add a new expense.

### Card: View Expenses
Description:Display a list of user expenses with filtering options.

### Card: Edit Expense
Description:Allow users to update existing expense records.

### Card: Delete Expense
Description:Allow users to remove an expense with confirmation.

## CATEGORIES & BUDGETS
### Card: Expense Categories
Description:
Allow users to assign categories to expenses.

### Card: Monthly Budget Setup
Description:
Allow users to define a monthly spending limit.

### Card: Budget vs Actual Comparison
Description:
Compare budgeted amount against actual expenses.

## REPORTS & VISUALIZATION
### Card: Monthly Expense Summary
Description:Display total expenses for a selected month.

### Card: Expense Breakdown by Category
Description:Group expenses by category and display totals.

### Card: Charts & Graphs
Description:Show charts (pie or bar) for spending visualization.

## USER INTERFACE & UX
### Card: Responsive Layout
Description:Ensure the app works on mobile, tablet, and desktop.

### Card: Navigation Menu
Description:Create clear navigation between pages.

### Card: Form Styling
Description:Improve form usability and visual consistency.

### Card: Validation Messages
Description:Display user-friendly validation and error messages.

## SECURITY & VALIDATION
### Card: Input Validation
Description:Validate user input to prevent invalid or harmful data.

### Card: Error Handling
Description:Gracefully handle errors without exposing sensitive information.

### Card: Secure Data Access
Description:Ensure secure database queries and user isolation.

## TESTING & QUALITY ASSURANCE
### Card: Feature Testing
Description:Test all CRUD operations and workflows.

### Card: Authentication Testing
Description:Verify login, logout, and access control.

### Card: Cross-Browser Testing
Description:Test app behavior in multiple browsers.

## DOCUMENTATION
### Card: README.md
Description:Write setup instructions and project overview.

### Card: User Guide
Description:Provide basic instructions for using the app.

### Card: Code Comments
Description:Add comments explaining important logic.

## DEPLOYMENT
### Card: Deployment Configuration
Description:Prepare the app for deployment.

### Card: Deploy to Cloud
Description: Deploy the app to a cloud service (if required).

### Card: Verify Deployment
Description:Confirm deployed app works correctly.

## FINAL SUBMISSION
### Card: Demo Video Preparation
Description:Plan and record the group demo video.

### Card: Final Review
Description:Verify all rubric requirements are met.

### Card: Submission to Canvas
Description:Submit required links and artifacts.

