# Kiseki Fighters
GMD1 Semester Project

Blog 1 

The day is 18/02/2024. I have finished the Roll-a-Ball tutorial, and have implemented a few extra features. However, most of them were learning about the usage of Free Assets, and making basic scripts like randomly generating trees and other nature structures, adding a skybox of sorts, and making a dynamic camera. 
Overall, this is my 2nd time playing around with Unity, as the first one was 1 year ago when I bought a course in Udemy, though I ended up not finishing it due to not finding time at the time. As such, it didn't feel so weird seeing how things worked, since I had already some kind of experience with the very basics.
Now taking all this information into context, and also remembering the fact that the final game project is to be made for an arcade, I have come up with 2 game ideas:
- Unethical Dario: Super Mario but full of dark humor, the idea would be a normal 2d platformer where we could choose between some "Dario's" characters, each based on countries, having their Pros and Cons (Example: American Dario, when getting the Star would get a Minigun, usual way to kill mobs is with a Glock-9), Mushrooms would make Dario get drugged and start seeing inverse with a rainbow filter on, The star would change the effect depending on each version of Dario, and the idea is that Dario is trying to save humans by trying to conquer the Planet of Coombas (in a proper colonizer fashion way kind of joke).
- Ikigai no Kiseki: A more serious take on the game, being this a 2d Arcade Fighter with a story to follow using Video Novels type of history. The main uniqueness of the game would be an attempt at making 2d textures on 3d characters, with your usual fighting system where each character has a skillset, possibly a different hitbox, an animated entrance and fatality, and so on.

I decided to go ahead with Ikigai no Kiseki since I feel like I'd be more invested in making a serious kind of game rather than a joke kind of game. The only reason why I only take these 2 ideas is because I feel like to produce a good game, you must first like the game. After watching the videos of the past student projects, all of them, I felt like most of the games lacked any kind of love behind them, probably because it is a university project rather than an actual world project, but I would like to attempt to use mine as an actual steam release in the future. As such, from another point of view, Unethical Dario would be a fun game to play but realistically would possibly get me involved in problems, while Ikigai no Kiseki would be completely fine.



Blog 2:
Kiseki - Game Design Document

High Level Concept/Design

Working Title
Kiseki Fighter
-	Kiseki being the Japanese word for Miracle, since one of the mechanics is that the Fighters get a “miracle” 2nd chance after dying.

Concept Statement
It’s a 2d Fighting Game for PvP fights, where each character has its own power and power-up. The fun concept is that you have a 2nd chance depending on how it goes.

Genre
Fighting and possibly Story. 

Target Audience
Competitive Fighting Game Fans and Anime Fans are the main target audience, though it can also be played by the public for a casual fun experience.

Unique Selling Points
The stand-out planned features compared to a normal fighting game will be:
-	After reaching a low % of health, the Player will automatically get a Power-Up that will have its own unique advantage depending on the character.
-	Each Character will have its own weapon and class.
-	Art style is expected to be somewhat different than the norm, resembling Anime/Manga art, kind of like Guilty Gear games, or Dragon Ball Fighter Z.


Product Design
Player Experience and Game POV
The player is whatever character he prefers. As of now, there is no story, since it will only be implemented if there is enough time.

Visual and Audio Style
For visuals there are already some very rough sketches:
 
 
As for Audio the idea is to have some anime like music to fit with the central art concept.

Game World Fiction
The game world is based on most fantasy worlds. There are races, where each work as a class. Human, Elf’s and Demons are the tree starting races/classes.
-	Humans: Balanced HP and Attack.
-	Elfs: Low HP, High Attack.
-	Demons: High HP, Low Attack.
Every being has a “Jinsei no Kiseki”, or Miracle of Life, where there is a hidden form that can only be reached when the user is close to its death. These power ups are unique to each character and based on their own background stories and biggest wishes/regrets.

Monetization
Future characters can be paid DLC’s, as different variations of a character can be paid DLC’s.
The idea is to first make the game be purchasable at the very minimum cost possible, and only after hooking a fanbase up, would the actual gains start.

Platforms, Technology and Scope
PC only. Rest of the stuff yet to be decided.

---------------------------------------

Blog Post 3

Milestone 1 : Movement

Starting with my game project after the first lessons, I decided to start with the basic movement, Jump and Move. I started by using the Old Player Input, which even though felt a bit easier to understand, ended up being replaced by me with the New Player Input System due to hearing about its positive effects. I had more of a hard time with things like setting up the gravity, rigid body configurations and such. Besides that it was pretty easy.

The problem was, after this, my laptop broke, and as I hadn't uploaded anything to GitHub by that time, I lost all of the work I had done. This meant I would have to get a new computer and restart. I tried to get the old laptop fixed but that took around 1 week and still didn't work. That said, I also had a bad timing of having planned vacations right after, which meant I could not redo the lost progress during that period.

But right after I was back, I started re doing the whole script from scratch. I made a test sprite for the player, and this time made movement with Jump, Move and Dash. I also made it so if you pressed the spacebar (jump) for longer, the player would jump longer.
With the movement now almost fully done, all was left was to configure the RigidBody2d and the Grounder function to avoid double jumping, as this required the player to be touching the ground in order to be able to jump.

With all set in stone, my next Milestone was decided to be UI's and Menus, as I was trying to follow the classes logic.

---------------------------------------

Blog Post 4

Milestone 2 : UI's and Menus

With the movement done, I decided to start working on the Main Menus and UI's of the game. First of I went for the Main Menu. After having a first iteration where I had this custom video wallpaper, based on my first idea of the game, which was an original story, I went to start doing the UI's. This is when I started noticing how it would be faster if I just got an already made story, and used it instead, as then I could save time on coming up with new characters, new backgrounds and styles. Thats when I chose one of my favorite animes Sousou no Frieren. With this decided, I had to go and re-do the Main Menu design. Changing Buttons to my new Buttons, Background and so on. I also added music to the Main Menu.

With Main Menu done, I went to work on the Character Selection Scenes. There are 2 character selection scenes, where the prefabs sprites are shown, as well as their name. I achieved this through using a database on unity, where I place all the prefabs that will be playable with. One thing is that I would have liked to have enough time to combine both character selection scenes into 1, as having 2 of them felt a bit time wasting for the player. I wanted the game to be easily accessible.

Character Selection done, I went on to work on UI's. 
For UI's its a simple combination of 2 bars, Health Bar and Stamina Bar, having both 100 of fill. These bars then have a script attached to move around with the values, get values and set values.

I would also like to note that, besides the background gif, the remaining sprites and pixel arts where all drawn by me.

---------------------------------------

Blog Post 5

Milestone 3 : Main Part of the Game 

With UI's set up and Menus done, it was time to complete the actual main part of the game.
I started by creating the scripts for players, as to finalize their prefabs. I created two classes, Elfs and Humans. There was plans to have a third class Tank, and there is script on it. This is also because the very first iteration of the prefab was a Human with Punch and Kick abilities. After some though I opted to make the human based on the character of the anime Himmel. This character has a sword, so having him punch would not make sense, thus I discarded it to become a Tank, while I would make humans have swords. With the human sword now done, I went on to make a new prefab with Elf skills. The Elf prefab has Magic and Kick. The Magic works by spawning a magic prefab that has a script to damage anything that touches but the caster. The magic also moves forward. It then destroys itself after 2 seconds, as thats more than enough to pass through the whole scenario.

With Elf and Human done, I went to add the transformation and guarding on each. Adding Guarding was a bit tricky since I tried to make calculations to remove stamina based on the attack damage. Transformation on the other hand was simpler. The damage of all attacks is boosted with Transformation, the HP and SP are refilled as to give more game time, and the "Punch" move is set to "Super Punch". I say punch because due to making it easier to be recognized by me, I kept the name Punch even if it was Magic or Sword. These transformed moves are prefabs on their own and have different hitbox and scripts.

Now having that completed, I went on for the hardest part of the project. The creation of the CharacterSpawner with management of input. This CharacterSpawner will spawn the selected prefabs, and then the Inputmanager will manually assign through code the devices to be used and their layouts. I say this part was the hardest because it was during the last days of the project, since I had been testing with keyboard and 2 different action maps, I went to the arcade and found out with 2 gamepads it was moving both players at same time. This meant I had to merge PlayerInputManager code with the CharacterSpawner in quick fashion.

--------------------------------------------

Blog Post 6

Final Product and Reflections

With the project now mostly completed, it was time to reflect.
To start with, the game is not exactly something I'm very proud of. Even though its a dream of mine to do one game, it is not something I can yet publish as I wished. It's fun, but not fun because its good, but fun because its that bad. It will take a laugh or two if you go into it expecting a high paced fighter game. There are many things I wish I could have more time to do better, but the reality is between the losing progress and the massive amount of assignments that I got during this Semester, I just couldn't find enough time nor energy to make the game better. One of the main parts I'm most regretful of, is the lack of characters. I originally intended to do 7. I did 2. Of course I should note EVERY character sprite was done completely by me. This proved to be a time taking task, even if fun. 
Besides that, the overall project is still looking like a playable game. I tried putting class mates playing it, and they seemed to enjoy it enough. It still has bugs here and there, but most of it is working as intended. There are some decisions that might be weird, but I felt like I had to take on since I didn't want to implement a timer. This meant I had to make it easier to defeat the enemy, and that's also why you can easily do a match in 40 seconds.

I intend to keep working on the project, as if possible I would like to release it some day in the internet. I would like to also mention that I posted a video of the gameplay for the teachers to see:
https://www.youtube.com/watch?v=0hLoLhAHQQw&t=55s
