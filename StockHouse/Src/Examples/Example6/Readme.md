# Example 6

Set a product composition sending it's id and a list of product id's and it's quantities. The list sent must override the previous product's content.

Things to consider:
- If the product to set composition doesn't exists, throws an exception
- Each Quantity of the list can't be negative or zero
- Each product id's of the list must exists
- If the list sent is empty, mark the product as **item product**
- If the list sent is not empty, mark the product as **non-item product**
- Remove previous content before setting the new content
- New composition can't generate a **composition loop**. For example, product 4 can't contains
product 5 (because product 5 contains product 4), product 8 (product 8 contains product 5, that contains product 4) or product 4 (loop of product 4 with product 4).
