## 2048_CS
Simple 2048 game written in C#. 

I asked the folks over at /r/csharp on reddit to tear my code apart. I will be going through the code and there anaylsis and recording the results/progress here. 

**Note: I am going the have two versions 2048.cs for the orginal and 2048_new.cs for the new updated version**

[Reddit Post Comments](http://www.reddit.com/r/csharp/comments/2tqlcm/my_first_c_program_2048_rip_it_apart_tell_me_what/)

Most of these edits will be the result of [/u/AngularBeginner's Comment](http://www.reddit.com/r/csharp/comments/2tqlcm/my_first_c_program_2048_rip_it_apart_tell_me_what/co1fthj)

I will be sourcing his/her comment as follows. (AngularBeginner, *line #'s*) And will do the same for other commenters.

###Step One: Naming

First I renamed the namespace so it won't start with a _, which was added by my IDE because one cannot start a namespace with a number. I renamed it as suggested "Game2048". (AngularBeginner, 2)  

Next instead of repeating the class name *Board*, I will instead be substituting it will *var*. I did this again on line 48. (AngularBeginner, [6,15]). 

**Varibles/Constants**
* t to zeroLoc: list of free spaces (AngularBeginner, 17)
* i to index in moveTile and RemoveTile Method (AngularBeginner, 27)
* m to end in moveTile and RemoveTile Method (AngularBeginner, 27)
* s to start in moveTile and RemoveTile Method (AngularBeginner, 27)
* Added two constants FEILD_MAX and FEILD_MIN (AngularBeginner, 25)
