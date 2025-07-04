# Simple E-Commerce Console App in C#

I built this console-based e-commerce application to practice C# programming concepts while creating something practical. It simulates a basic online store where you can browse products, manage a cart, and complete purchases - all through a text interface.

# Core Features

# 1. Product Catalog
- View all available products with details like price, stock, and weight
- Products marked with expiration dates won't appear if expired
- Clear indicators for items that require shipping

# 2. Shopping Cart System
- Add/remove products with quantity control
- Real-time stock updates when modifying cart
- Automatic calculation of subtotals and shipping fees

# 3. Customer Management
- Tracks available balance
- Validates funds before purchase
- Handles refunds when removing items

# 4. Shipping Logic
- $30 flat fee for shippable items
- Total weight calculation
- Clear shipping notifications during checkout

# 5. Robust Checkout Process
- Multiple validation checks:
  - Non-empty cart
  - Sufficient funds
  - No expired products
- Detailed receipt generation

# Technical Implementation

The application follows a clean structure:

```
E-Commerce/
├── Models/                # Data models
│   ├── Product.cs         # Product with name, price, stock, shipping, weight, expiry
│   ├── Customer.cs        # Customer with name and balance
│   ├── CartItem.cs        # Cart item with product and quantity
│   └── ShoppingCart.cs    # Shopping cart with add/remove/checkout logic
├── Services/              # Business logic
│   ├── ProductService.cs  # Product display and selection
│   ├── CartService.cs     # Cart operations and validation
│   └── CheckoutService.cs # Checkout process and validation
├── Interface/             # User interface
|    ├── InputHandler.cs   # Input validation and user prompts
│    └── MainMenu.cs       # Main menu and navigation
├── Program.cs             # Application entry point
└── README.md              # Project documentation
```

Key technical aspects:

- Object-oriented design with proper separation of concerns
- Input validation for all user interactions
- Graceful error handling throughout
- Menu-driven navigation
- Clear user feedback at every step

# Why I Built It This Way

I wanted to create something that:
- Actually works like a real e-commerce system
- Handles edge cases properly (like expired products)
- Provides clear feedback to users
- Demonstrates good coding practices

The console interface keeps things simple while allowing me to focus on the core logic. I might expand this to a GUI version later, but for now it serves as a solid foundation.

# Challenges Solved

Some interesting problems I tackled:
- Managing inventory synchronization between catalog and cart
- Calculating shipping only for relevant items
- Handling currency values precisely
- Creating a user-friendly interface in a console environment
- Implementing comprehensive input validation

The code includes plenty of comments explaining the thought process behind key decisions.