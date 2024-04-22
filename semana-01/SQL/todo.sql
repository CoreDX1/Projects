create table account (
	user_id SERIAL PRIMARY KEY,
	user_name VARCHAR(200) NOT NULL,
	password INTEGER NOT NULL,
	email  VARCHAR(100) UNiQUE NOT NULL,
	create_at TIMESTAMP NOT NULL
)

INSERT INTO account (user_name, password, email, create_at)
VALUES
    ('John Doe', 123456, 'johndoe@example.com', NOW()),
    ('Jane Smith', 654321, 'janesmith@example.com', NOW()),
    ('Bob Johnson', 111111, 'bobjohnson@example.com', NOW()),
    ('Alice Brown', 789012, 'alicebrown@example.com', NOW()),
    ('Mike Davis', 246810, 'mikedavis@example.com', NOW());

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

INSERT INTO task (user_id, title, description, due_date, completed)
VALUES
    (1, 'Task 1 for John Doe', 'This is the first task for John Doe', '2023-03-15', FALSE),
    (1, 'Task 2 for John Doe', 'This is the second task for John Doe', '2023-03-20', FALSE),
    (2, 'Task 1 for Jane Smith', 'This is the first task for Jane Smith', '2023-03-10', FALSE),
    (2, 'Task 2 for Jane Smith', 'This is the second task for Jane Smith', '2023-03-25', FALSE),
    (3, 'Task 1 for Bob Johnson', 'This is the first task for Bob Johnson', '2023-03-12', FALSE),
    (3, 'Task 2 for Bob Johnson', 'This is the second task for Bob Johnson', '2023-03-28', FALSE),
    (4, 'Task 1 for Alice Brown', 'This is the first task for Alice Brown', '2023-03-18', FALSE),
    (4, 'Task 2 for Alice Brown', 'This is the second task for Alice Brown', '2023-03-22', FALSE),
    (5, 'Task 1 for Mike Davis', 'This is the first task for Mike Davis', '2023-03-05', FALSE),
    (5, 'Task 2 for Mike Davis', 'This is the second task for Mike Davis', '2023-03-30', FALSE);
	
	
select 
	c.user_name, 
	t.title, 
	t.description 
from account AS c INNER JOIN task AS t ON t.user_id = c.user_id where c.user_name LIKE 'Alice%';