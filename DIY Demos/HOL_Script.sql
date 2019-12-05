CREATE TABLE usertable(id int NOT NULL IDENTITY PRIMARY KEY, name varchar(100), Phone varchar(15), gender varchar(12), email varchar(100), faceid varchar(100));

CREATE TABLE imagevalidation(id int NOT NULL IDENTITY PRIMARY KEY, validation_type varchar(100), validation_message varchar(255),isactive int);

CREATE TABLE gesture(id int NOT NULL IDENTITY PRIMARY KEY, gesture_name varchar(100), thumbnail_url varchar(max), gesture_message varchar(255), isactive int);

CREATE TABLE auditlog(id int NOT NULL IDENTITY PRIMARY KEY, layer varchar(100), result_type varchar(50), device_type varchar(50), userimage text);

CREATE TABLE verifytime(id int NOT NULL IDENTITY PRIMARY KEY, personid varchar(100), date varchar(25), checkin varchar(25), checkout varchar(25));


INSERT INTO imagevalidation(validation_type, validation_message, isactive) VALUES('Face Availability','Face is not available',0);
INSERT INTO imagevalidation(validation_type, validation_message, isactive) VALUES('Remove Sun Glasses','Please remove sunglasses',0);
INSERT INTO imagevalidation(validation_type, validation_message, isactive) VALUES('Multiple Face','Multiple Faces are detected',0);
INSERT INTO imagevalidation(validation_type, validation_message, isactive) VALUES('Expressions','Expression is not Neutral',0);

INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('One','https://aitechstorageaccount.blob.core.windows.net/aiimages/1.JPEG?sp=r&st=2019-09-20T11:29:16Z&se=2019-10-19T19:29:16Z&spr=https&sv=2018-03-28&sig=e3qaVe8qFdDh4h0fbQlmUVua1D%2BlJvwmIe3uL5tzw8Q%3D&sr=b','The Person is showing one',0);
INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('Two','https://aitechstorageaccount.blob.core.windows.net/aiimages/2.JPEG?sp=r&st=2019-09-20T11:30:21Z&se=2019-10-19T19:30:21Z&spr=https&sv=2018-03-28&sig=eNXBhQANzVxsEzX9xnvAoEpxGk9GVamm5yry6Nya9tI%3D&sr=b','The Person is showing Two',0);
INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('Three','https://aitechstorageaccount.blob.core.windows.net/aiimages/3.JPEG?sp=r&st=2019-09-20T11:31:16Z&se=2019-10-19T19:31:16Z&spr=https&sv=2018-03-28&sig=7kZS9KAWUcz0tPxQsmDoZ9eqFak1pR%2FpkMuOa3A95KA%3D&sr=b','The Person is showing Three',0);
INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('Four','https://aitechstorageaccount.blob.core.windows.net/aiimages/4.JPEG?sp=r&st=2019-09-20T11:31:43Z&se=2019-10-19T19:31:43Z&spr=https&sv=2018-03-28&sig=R7JM1i%2BZS%2FzvFSNzTYebUbxg0IFVkA7NAfR8ooZMbzY%3D&sr=b','The Person is showing Four',0);
INSERT INTO gesture(gesture_name, thumbnail_url, gesture_message, isactive) VALUES('Five','https://aitechstorageaccount.blob.core.windows.net/aiimages/5.JPEG?sp=r&st=2019-09-20T11:32:25Z&se=2019-10-19T19:32:25Z&spr=https&sv=2018-03-28&sig=bjlvQjRj%2BwKnu%2FcUv6e0UjqORm6E9rgOUud2LxCVTxs%3D&sr=b','The Person is showing Five',0);