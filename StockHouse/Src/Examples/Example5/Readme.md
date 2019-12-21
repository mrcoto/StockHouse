# Example 5

Given a product id and a warehouse id, return it's **physical or logical stock**.

- Should throw exception if product doesn't exists
- Should throw exception if warehouse doesn't exists

For example:

- For (Product ID, Warehouse ID) = (1, 1), the physical stock is 50 (physical stock in warehouse)
- For (Product ID, Warehouse ID) = (1, 2), the physical stock is 16 (physical stock in warehouse)

But, with **non-item products** the logical stock is based on it's item composition.

- For (Product ID, Warehouse ID) = (8, 1), the logical stock is 5.
Because:
    - Item composition is: 10 x Product 1 and 3 x Product 4
    - Stock of product 1 is 50 and stock of product 4 is 20 (In warehouse 1)


