-- Warehouses
insert into warehouse(name, created_at, updated_at) values ('Warehouse #1', NOW(), NOW());
insert into warehouse(name, created_at, updated_at) values ('Warehouse #2', NOW(), NOW());

-- Products
insert into product(alias_name, name, is_item, created_at, updated_at) values
('keyboardgamer', 'Corsair K95 RGB Platinum', true, NOW(), NOW()), -- ID: 1
('keyboard', 'Generic Keyboard', true, NOW(), NOW()), -- ID: 2
('mouse', 'Generic Mouse', true, NOW(), NOW()), -- ID: 3
('printer', 'Printer', true, NOW(), NOW()), -- ID: 4
('pack1', 'Pack #1: 2 x Keyboard Gamer & Printer', false, NOW(), NOW()), -- ID: 5
('pack2', 'Pack #2: 3 x Generic Mouse', false, NOW(), NOW()), -- ID: 6
('pack3', 'Pack #3: 2 x Pack #1 & Pack # 2 & 3 x Generic Keyboard', false, NOW(), NOW()), -- ID: 7
('pack4', 'Pack #4: 3 x Pack #1 & 4 x Keyboard Gamer', false, NOW(), NOW()), -- ID: 8
('pack5', 'Pack #5: 4 x Pack #4 & 2 x Pack #3', false, NOW(), NOW()) -- ID: 9
;

-- Warehouse has product
-- There is no 'Printer' in Warehouse #2 (no record) & Stock 0 of 'Generic Keyboard' in Warehouse #2
insert into warehouse_has_product(warehouse_id, product_id, stock) values
(1, 1, 50),
(1, 2, 100),
(1, 3, 50),
(1, 4, 20),
(2, 1, 16),
(2, 2, 0),
(2, 3, 55)
;

-- Product has Product
insert into product_has_product(product_id, product_content_id, quantity) values
(5, 1, 2),
(5, 4, 1),
(6, 3, 3),
(7, 5, 2),
(7, 6, 1),
(7, 2, 3),
(8, 5, 3),
(8, 1, 4),
(9, 8, 4),
(9, 7, 2)
;