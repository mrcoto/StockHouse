# Example 2

Given a Product Id and a Tree Level, Get the Product Data and It's Composition in tree view.

For example (JSON View to understand):

- **Input: 7, Level 1**

```json
[
    {
        "Id": 5,
        "AliasName": "pack1",
        "Name": "Pack #1: 2 x Keyboard Gamer & Printer",
        "Quantity": 2,
        "Composition": [
            // Only One Level
        ]
    },
    {
        "Id": 6,
        "AliasName": "pack2",
        "Name": "Pack #2: 3 x Generic Mouse",
        "Quantity": 1,
        "Composition": [
            // Only One Level
        ]
    },
    {
        "Id": 2,
        "AliasName": "keyboard",
        "Name": "Generic Keyboard",
        "Quantity": 3,
        "Composition": [] // No Composition
    }
]
```

- **Input: 7, Level 2**

```jsonc
[
    {
        "Id": 5,
        "AliasName": "pack1",
        "Name": "Pack #1: 2 x Keyboard Gamer & Printer",
        "Quantity": 2,
        "Composition": [
            {
                "Id": 1,
                "AliasName": "keyboardgamer",
                "Name": "Corsair K95 RGB Platinum",
                "Quantity": 2,
                "Composition": [] // No Composition
            },
            {
                "Id": 4,
                "AliasName": "printer",
                "Name": "Printer",
                "Quantity": 1,
                "Composition": [] // No Composition
            },
        ]
    },
    {
        "Id": 6,
        "AliasName": "pack2",
        "Name": "Pack #2: 3 x Generic Mouse",
        "Quantity": 1,
        "Composition": [
            {
                "Id": 3,
                "AliasName": "mouse",
                "Name": "Generic Mouse",
                "Quantity": 3,
                "Composition": [] // No Composition
            },
        ]
    },
    {
        "Id": 2,
        "AliasName": "keyboard",
        "Name": "Generic Keyboard",
        "Quantity": 3,
        "Composition": [] // No Composition
    }
]
```

**Note:** This Product has two level max, if level input is 100, the response would be equal.