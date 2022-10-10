CREATE DATABASE enjoffer;

CREATE TYPE professions_type AS ENUM('Програміст', 'Лікар', 'Математик');

CREATE DOMAIN password_size AS VARCHAR(255) CHECK(VALUE LIKE '______%');

CREATE TABLE professions(profession_id SERIAL, profession_name professions_type);

CREATE TABLE users(
	user_id SERIAL PRIMARY KEY,
	username VARCHAR(255) NOT NULL UNIQUE,
	user_password password_size NOT NULL,
	profession professions_type
);

CREATE TABLE advices(
	advice_id SERIAL PRIMARY KEY,
	advice_name TEXT NOT NULL
);

ALTER TABLE advices
ADD CONSTRAINT advice_unique UNIQUE(advice_name);

CREATE TABLE books(
	book_id SERIAL PRIMARY KEY,
	title VARCHAR(255) NOT NULL,
	description TEXT,
	book_content TEXT NOT NULL,
	number_of_pages INT DEFAULT 1,
	book_cover_img VARCHAR(255),
	last_viewed_page INT
);

ALTER TABLE books
ADD CONSTRAINT book_unique UNIQUE(title);

CREATE TABLE sentences(
	sentence_id SERIAL PRIMARY KEY,
	sentence TEXT NOT NULL
);

ALTER TABLE sentences
ADD CONSTRAINT sentence_unique UNIQUE(sentence);

ALTER TABLE books
ADD COLUMN fk_sentence_id INT;

ALTER TABLE books
ADD COLUMN author VARCHAR(255);

ALTER TABLE books
ADD CONSTRAINT fk_book_sentence FOREIGN KEY(fk_sentence_id) REFERENCES sentences(sentence_id)

CREATE TABLE words(
	word_id SERIAL PRIMARY KEY,
	word VARCHAR(255) NOT NULL,
	word_translation VARCHAR(255) NOT NULL,
	image_src VARCHAR(255),
	date DATE,
	is_correct_inputed BOOLEAN,
	correct_times_inputed INT,
	incorrect_times_inputed INT
);

ALTER TABLE words
ADD CONSTRAINT word_unique UNIQUE(word);

CREATE TABLE user_book(
	user_id INT REFERENCES users(user_id),
	book_id INT REFERENCES books(book_id),
	
	CONSTRAINT user_book_pkey PRIMARY KEY(user_id, book_id)
);

CREATE TABLE user_word(
	user_id INT REFERENCES users(user_id),
	word_id INT REFERENCES words(word_id),
	
	CONSTRAINT user_word_pkey PRIMARY KEY(user_id, word_id)
);

CREATE TABLE user_advice(
	user_id INT REFERENCES users(user_id),
	advice_id INT REFERENCES advices(advice_id),
	
	CONSTRAINT user_advice_pkey PRIMARY KEY(user_id, advice_id)
);

CREATE TABLE sentence_word(
	sentence_id INT REFERENCES sentences(sentence_id),
	word_id INT REFERENCES words(word_id),
	
	CONSTRAINT sentence_word_pkey PRIMARY KEY(sentence_id, word_id)
);

CREATE OR REPLACE FUNCTION calc_pages()
RETURNS trigger AS
$$
BEGIN
	NEW.number_of_pages = CEIL(char_length(NEW.book_content) / 500);
	RETURN NEW;
END;
$$
LANGUAGE plpgsql;

CREATE TRIGGER calculated_pages BEFORE INSERT OR UPDATE ON books
FOR EACH ROW EXECUTE PROCEDURE calc_pages();