CREATE TABLE pdi_engineer_details(id int NOT NULL IDENTITY PRIMARY KEY, name varchar(100));

CREATE TABLE Task(id int NOT NULL IDENTITY PRIMARY KEY, task varchar(1000), pdi_engineer_id int FOREIGN KEY REFERENCES pdi_engineer_details(id), task_datetime datetime, isapproved int, isrejected int, ispaymentmade int);

CREATE TABLE attendance(id int NOT NULL IDENTITY PRIMARY KEY, pdi_engineer_id int FOREIGN KEY REFERENCES pdi_engineer_details(id), checkin datetime, checkout datetime);