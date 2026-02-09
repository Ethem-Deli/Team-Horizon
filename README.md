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


# Family Budget & Expense Tracker - Plain Language Description
## What It Does
The Family Budget & Expense Tracker is a personal finance management web application that helps individuals and families track their spending, set budgets, and understand where their money goes.

## Primary Function
Think of it as a digital financial diary that:

Records every expense you make (like buying groceries, paying bills, or going to the movies)
Organizes expenses into categories (food, transportation, entertainment, etc.)
Compares your spending against budgets you set for yourself
Shows you visual reports so you can see spending patterns

## How It Works (User Journey)
Step 1: Get Started

You create an account and log in
The system gives you a personal dashboard

Step 2: Set Up Categories

You create categories that match your life (e.g., "Groceries," "Gas," "Netflix")
Each category gets a color and icon for easy recognition

Step 3: Record Expenses
When you spend money, you:

Enter the amount (e.g., $45.50)
Choose the date
Pick a category
Add a description (e.g., "Weekly groceries at Walmart")
Click "Save"

Step 4: Set Budgets

You decide: "I want to spend only $2,000 this month"
Or get specific: "Only $400 on groceries this month"
The app tracks how much you've spent vs. your limit

Step 5: View Reports
The app shows you:

Monthly summaries: "You spent $1,850 in January"
Category breakdowns: "40% went to food, 25% to rent, 15% to entertainment"
Budget warnings: "You're at 85% of your grocery budget!"
Charts and graphs: Visual pie charts showing spending distribution

# What It Intends to Achieve
1. Financial Awareness
Problem: Most people don't know where their money goes
Solution: See exactly how much you spend and on what
Example: "I didn't realize I spend $200/month on coffee shops!"

2. Budget Control
Problem: Overspending without realizing it
Solution: Set limits and get warnings before you overspend
Example: The app shows you're at 90% of your entertainment budget with 10 days left in the month

3. Spending Patterns
Problem: Can't identify wasteful spending habits
Solution: Visual reports reveal trends over time
Example: Charts show your restaurant spending increased 30% over 3 months

4. Financial Planning
Problem: Hard to plan for the future without historical data
Solution: Use past spending to plan realistic future budgets
Example: "I spent an average of $500/month on groceries, so I'll budget $550 next month"

5. Financial Accountability
Problem: Shared finances are hard to track
Solution: One place where family members can see all expenses
Example: Both partners can see household spending and stay on the same page

# Real-World Scenario
Meet Sarah:

She feels like money "just disappears" every month
She's trying to save for a vacation but never has money left over

Using the App:
Month 1:

Sarah records all her expenses for 30 days
She's shocked to see she spent $300 on food delivery apps
She spent $150 on subscriptions she forgot about

Month 2:

Sarah sets a budget: $2,500 total, with $400 for groceries
She creates categories for each type of spending
The app warns her when she's close to limits

Month 3:

Sarah reviews her reports
She sees her "impulse shopping" category is too high
She cancels unused subscriptions
She starts cooking more (food delivery drops to $100)

Result:

Sarah saves $400 in one month
She books her vacation 6 months later
She feels in control of her finances

# Core Goals
Short-term Goals:

✅ Track every dollar spent
✅ Categorize expenses automatically
✅ Know your spending in real-time
✅ Stay within monthly budgets

Long-term Goals:

💰 Build better spending habits
💰 Identify and eliminate wasteful spending
💰 Save money consistently
💰 Achieve financial goals (vacation, emergency fund, debt payoff)


🛠️ Technical Function (Simplified)
Behind the Scenes:

You enter data → Stored in a database
The app calculates → Totals, percentages, comparisons
You view results → Dashboard, reports, charts
The cycle repeats → Continuous tracking and improvement

Key Features:

CRUD Operations: Create, Read, Update, Delete expenses
Data Organization: Group by category, date, amount
Calculations: Totals, averages, percentages
Visualization: Turn numbers into understandable charts
Security: Each user only sees their own data

# 📈 Success Metrics
The app is successful when users:

✅ Know their spending: Can tell you how much they spent last month
✅ Stay on budget: Spend less than budgeted amounts
✅ Identify problems: Discover wasteful spending patterns
✅ Make changes: Adjust behavior based on data
✅ Save money: Have money left over at month's end

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
-----------------------------------------------
Depencies to install 
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Design
to restore packages
dotnet resRebuild the project

Rebuild the project
dotnet build

