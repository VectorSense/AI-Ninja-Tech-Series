CREATE TABLE usertable(id int NOT NULL IDENTITY PRIMARY KEY, name varchar(100), Phone varchar(15), gender varchar(12), email varchar(100), faceid varchar(100));

CREATE TABLE imagevalidation(id int NOT NULL IDENTITY PRIMARY KEY, validation_type varchar(100), validation_message varchar(255),isactive int);

CREATE TABLE gesture(id int NOT NULL IDENTITY PRIMARY KEY, gesture_name varchar(100), thumbnail_url varchar(max), gesture_message varchar(255), isactive int);

CREATE TABLE auditlog(id int NOT NULL IDENTITY PRIMARY KEY, layer varchar(100), result_type varchar(50), device_type varchar(50), userimage text);

CREATE TABLE verifytime(id int NOT NULL IDENTITY PRIMARY KEY, personid varchar(100), date varchar(25), checkin varchar(25), checkout varchar(25));


INSERT INTO imagevalidation(validation_type, validation_message, isactive) VALUES('Face Availability','Face is not available',0);
INSERT INTO imagevalidation(validation_type, validation_message, isactive) VALUES('Remove Sun Glasses','Please remove sunglasses',0);
INSERT INTO imagevalidation(validation_type, validation_message, isactive) VALUES('Multiple Face','Multiple Faces are detected',0);
INSERT INTO imagevalidation(validation_type, validation_message, isactive) VALUES('Expressions','Expression is not Neutral',0);

INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('One','https://msspeechtext.blob.core.windows.net/msspeechtext/one.jpg?sp=r&st=2019-12-30T11:45:30Z&se=2019-12-30T19:45:30Z&spr=https&sv=2019-02-02&sr=b&sig=q%2F1xsXbHOtQKu7n7hL4AdF4KCp6RRVuw5CGU510Xjho%3D','The Person is showing one',0);
INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('Two','https://msspeechtext.blob.core.windows.net/msspeechtext/two.jpg?sp=r&st=2019-12-30T11:47:39Z&se=2019-12-30T19:47:39Z&spr=https&sv=2019-02-02&sr=b&sig=gKuM9Ul4Y40tNkofrmnz%2F1u4%2B3BKcVonFuQ8Fe7PiVg%3D','The Person is showing Two',0);
INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('Three','https://msspeechtext.blob.core.windows.net/msspeechtext/three.jpg?sp=r&st=2019-12-30T11:48:33Z&se=2019-12-30T19:48:33Z&spr=https&sv=2019-02-02&sr=b&sig=XLF1GWRusREKBKm9P%2Bu%2B81OArGhS0%2F7sF%2Bd9naqgbMQ%3D','The Person is showing Three',0);
INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('Four','https://msspeechtext.blob.core.windows.net/msspeechtext/four.jpg?sp=r&st=2019-12-30T11:49:10Z&se=2019-12-30T19:49:10Z&spr=https&sv=2019-02-02&sr=b&sig=1%2Fsb8F16DQr5oO34cDOSyHCD4BX3hUk0gUiNuAAGitQ%3D','The Person is showing Four',0);
INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('Five','https://msspeechtext.blob.core.windows.net/msspeechtext/five.jpg?sp=r&st=2019-12-30T11:49:52Z&se=2019-12-30T19:49:52Z&spr=https&sv=2019-02-02&sr=b&sig=os4792BFtIRzy71mwE2akwfKg46%2FvW5AZzBrCBF5Jlc%3D','The Person is showing Five',0);