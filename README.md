# University System
=======
 

### 

What exactly does this project ? 
###
 
This project has been created with educational purposes and representation of skills. It is fully functional.
It is a ASP.NET MVC5 application using Entity Framework Code First and LINQ expressions which implements several design 
patterns(Unit of work, Repository, MVC). 

This system is designed for managing university structure of courses, subjects, teachers, students, grades and information section.
Course has unique name and faculty namber which students get when they are assigned to it. 
Subjects has unique name and one or many courses which could be assignet to and every student in these courses could receive grade.
Teachers has assigned title and subject which lead and set grades to students in it.
Students can log in the system with there faculty number (stu12345678) and check their grades and change password.
Administrators manage the CRUD operations but can't set grades to the students. 

Information section is managed by administrators and can be seen from every logged user. Every article in this section could have one or many images and one file attached to it.

How to run
------------
Open the application and run it.
Seeded default administrator -  admin/password

Default password for every new teacher is "password" 

Username of student is "stu" + his faculty number and password by default is "password"

