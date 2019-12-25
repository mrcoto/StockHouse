drop table if exists product_has_product;
drop table if exists warehouse_has_product;
drop table if exists product;
drop table if exists warehouse;

create table warehouse(
    id serial primary key,
    name varchar(20),
    created_at timestamp default current_timestamp,
    updated_at timestamp default current_timestamp
);

create table product(
    id serial primary key,
    alias_name varchar(30),
    name varchar(120),
    is_item boolean,
    created_at timestamp default current_timestamp,
    updated_at timestamp default current_timestamp
);

create table warehouse_has_product(
    warehouse_id int,
    product_id int,
    stock int default 0,
    primary key(warehouse_id, product_id),
    foreign key(warehouse_id) references warehouse(id),
    foreign key(product_id) references product(id)
);

create table product_has_product(
    product_id int,
    product_content_id int,
    quantity int,
    primary key(product_id, product_content_id),
    foreign key(product_id) references product(id),
    foreign key(product_content_id) references product(id)
);

-- Warehouses
insert into warehouse(name) values ('Warehouse #1');
insert into warehouse(name) values ('Warehouse #2');

-- Products
insert into product(alias_name, name, is_item) values
('keyboardgamer', 'Corsair K95 RGB Platinum', true), -- ID: 1
('keyboard', 'Generic Keyboard', true), -- ID: 2
('mouse', 'Generic Mouse', true), -- ID: 3
('printer', 'Printer', true), -- ID: 4
('pack1', 'Pack #1: 2 x Keyboard Gamer & Printer', false), -- ID: 5
('pack2', 'Pack #2: 3 x Generic Mouse', false), -- ID: 6
('pack3', 'Pack #3: 2 x Pack #1 & Pack # 2 & 3 x Generic Keyboard', false), -- ID: 7
('pack4', 'Pack #4: 3 x Pack #1 & 4 x Keyboard Gamer', false) -- ID: 8
('pack5', 'Pack #5: 4 x Pack #4 & 2 x Pack #3', false) -- ID: 9
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