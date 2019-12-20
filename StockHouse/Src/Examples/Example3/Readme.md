# Example 3

Given a Product Id, Return the **item product** composition list (only leaf nodes from tree composition at max level)

For example (JSON View to understand):

- **Input: 7**

```json
[
    {
        "Id": 1,
        "AliasName": "keyboardgamer",
        "Name": "Corsair K95 RGB Platinum",
        "Quantity": 4,
    },
    {
        "Id": 4,
        "AliasName": "printer",
        "Name": "Printer",
        "Quantity": 2,
    },
    {
        "Id": 3,
        "AliasName": "mouse",
        "Name": "Generic Mouse",
        "Quantity": 3,
    },
    {
        "Id": 2,
        "AliasName": "keyboard",
        "Name": "Generic Keyboard",
        "Quantity": 3,
    }
]
```

- **Input: 8**

1	keyboardgamer	Corsair K95 RGB Platinum	true
2	keyboard	Generic Keyboard	true
3	mouse	Generic Mouse	true
4	printer	Printer	true

```jsonc
[
    {
        "Id": 1,
        "AliasName": "keyboardgamer",
        "Name": "Corsair K95 RGB Platinum",
        // This product has 4 keyboards gamer
        // And 3 x pack #1
        // Also, Each pack #1 has 2 keyboard gamers
        // So, Quantity is 3 * 2 + 4 => 10
        "Quantity": 10,
    },
    {
        "Id": 4,
        "AliasName": "printer",
        "Name": "Printer",
        "Quantity": 3,
    }
]
```