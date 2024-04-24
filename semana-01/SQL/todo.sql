create table account (
	user_id SERIAL PRIMARY KEY,
	user_name VARCHAR(200) NOT NULL,
	password VARCHAR(25) NOT NULL,
	email  VARCHAR(100) UNiQUE NOT NULL,
	create_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
)


INSERT INTO account (user_name, password, email) VALUES 
('John Doe', 'password123', 'johndoe@example.com'),
('Jane Smith', 'ilovecats', 'janesmith@example.com'),
('Bob Johnson', 'qwertyuiop', 'bobjohnson@example.com'),
('Alice Brown', 'letmein', 'alicebrown@example.com'),
('Mike Davis','mike123','mikedavis@example.com');

select * from account;

create table task (
	id SERIAL PRIMARY KEY,
	user_id INTEGER NOT NULL,
	title VARCHAR(100) NOT NULL,
	description TEXT,
	due_date DATE,
	completed BOOLEAN NOT NULL DEFAULT FALSE,
	crated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	update_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	CONSTRAINT fk_account FOREIGN KEY (user_id) REFERENCES "account"(user_id)
)

INSERT INTO task (user_id, title, description, due_date) VALUES 
-- John Doe's tasks
(1, 'Task 1', 'This is task 1', '2022-01-01'),
(1, 'Task 2', 'This is task 2', '2022-01-05'),
(1, 'Task 3', 'This is task 3', '2022-01-10'),
(1, 'Task 4', 'This is task 4', '2022-01-15'),
(1, 'Task 5', 'This is task 5', '2022-01-20'),
(1, 'Task 6', 'This is task 6', '2022-01-25'),
(1, 'Task 7', 'This is task 7', '2022-02-01'),
(1, 'Task 8', 'This is task 8', '2022-02-05'),
(1, 'Task 9', 'This is task 9', '2022-02-10'),
(1, 'Task 10', 'This is task 10', '2022-02-15'),

-- Jane Smith's tasks
(2, 'Task 1', 'This is task 1', '2022-01-02'),
(2, 'Task 2', 'This is task 2', '2022-01-07'),
(2, 'Task 3', 'This is task 3', '2022-01-12'),
(2, 'Task 4', 'This is task 4', '2022-01-17'),
(2, 'Task 5', 'This is task 5', '2022-01-22'),
(2, 'Task 6', 'This is task 6', '2022-01-27'),
(2, 'Task 7', 'This is task 7', '2022-02-02'),
(2, 'Task 8', 'This is task 8', '2022-02-07'),
(2, 'Task 9', 'This is task 9', '2022-02-12'),
(2, 'Task 10', 'This is task 10', '2022-02-17'),

-- Bob Johnson's tasks
(3, 'Task 1', 'This is task 1', '2022-01-03'),
(3, 'Task 2', 'This is task 2', '2022-01-08'),
(3, 'Task 3', 'This is task 3', '2022-01-13'),
(3, 'Task 4', 'This is task 4', '2022-01-18'),
(3, 'Task 5', 'This is task 5', '2022-01-23'),
(3, 'Task 6', 'This is task 6', '2022-01-28'),
(3, 'Task 7', 'This is task 7', '2022-02-03'),
(3, 'Task 8', 'This is task 8', '2022-02-08'),
(3, 'Task 9', 'This is task 9', '2022-02-13'),
(3, 'Task 10', 'This is task 10', '2022-02-18'),

-- Alice Brown's tasks
(4, 'Task 1', 'This is task 1', '2022-01-04'),
(4, 'Task 2', 'This is task 2', '2022-01-09'),
(4, 'Task 3', 'This is task 3', '2022-01-14'),
(4, 'Task 4', 'This is task 4', '2022-01-19'),
(4, 'Task 5', 'This is task 5', '2022-01-24'),
(4, 'Task 6', 'This is task 6', '2022-01-29'),
(4, 'Task 7', 'This is task 7', '2022-02-04'),
(4, 'Task 8', 'This is task 8', '2022-02-09'),
(4, 'Task 9', 'This is task 9', '2022-02-14'),
(4, 'Task 10', 'This is task 10', '2022-02-19'),

-- Mike Davis's tasks
(5, 'Task 1', 'This is task 1', '2022-01-06'),
(5, 'Task 2', 'This is task 2', '2022-01-11'),
(5, 'Task 3', 'This is task 3', '2022-01-16'),
(5, 'Task 4', 'This is task 4', '2022-01-21'),
(5, 'Task 5', 'This is task 5', '2022-01-26'),
(5, 'Task 6', 'This is task 6', '2022-01-31'),
(5, 'Task 7', 'This is task 7', '2022-02-05'),
(5, 'Task 8', 'This is task 8', '2022-02-10'),
(5, 'Task 9', 'This is task 9', '2022-02-15'),
(5, 'Task 10', 'This is task 10', '2022-02-20');
	
	
select 
	c.user_name, 
	t.title, 
	t.description 
from account AS c INNER JOIN task AS t ON t.user_id = c.user_id where c.user_name LIKE 'Alice%';


select * from account;