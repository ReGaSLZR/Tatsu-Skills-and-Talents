A simple project I made to make it easier for a Game Designer to:
> mix-and-match values

> preview said values in order to test their designs for character skills and talents (skill modifiers)

> also to give designers the opportunity to save and load their work for eventual in-game implementations


# NOTES:
## **[!] this project is NOT meant to be exported to any platform. Please only run it inside Unity [!]**
- you can control the movement of the Playable Character using your arrow keys
- this project relies on ScriptableObjects to store and load data in runtime
- uses just one Scene

# Project Implementation steps I did:
> formulated the UML of my base models Skill and Talent, populated their attributes and saw how they would connect

> arranged features I have in mind from the highest to the lowest priority (like: Skills being able to accommodate any number of Talents using an okay UI/UX is higher as a priority than adding the Save and Load feature)

> plotted simple UI/UX designs for my features in Unity

> wrote the codes for parsing UI input into class instances

> wrote the codes for the in-game characters and used the class instances on them

> did a lot of playtesting and UI redesigning

> wrote the codes for the parsing of class instances into UI input for the Save/Load feature

> disabled Skills and Talent attributes related to projectiles to make time for more playtesting, UI/UX redesigning and video recordings


# CODING PRACTICES I enforced:
- SOLID, particularly Single Responsibility
- made sure every script is namespaced but is also less than 300 lines
- clear and concise naming of classes and methods
- OOP concepts, especially Abstraction, Encapsulation, Inheritance
- extension methods
- personally, I'm more into Dependency Injection (using Zenject) and Reactive Programming (using UniRx) but for this project, I used Coroutines + Pub-Sub and Singleton design patterns to get away from plugins


# ASSET PACKS I used:
- JMO Assets' Cartoon FX (for my VFX prefabs)
- GUI Pro Kit - Fantasy RPG (for my UI designs)


# Other tools / resources I used:
- itch.io for free 2D sprites
- freesound.org for free SFXs
- Unity Cinemachine just for a simple Camera that follows a target with a specific damping and screen bias. Nothing complex.
- Unity Timeline for simple UI stat change animations (specifically for the animating "+### Stat" text on the characters in-game)
- Unity TextMeshPro for crispier UI Texts
- Unity 2D Sprite for slicing spritesheets
- SMB's StandaloneFileBrowser https://github.com/gkngkc/UnityStandaloneFileBrowser for the file loading prompt in Unity


# LINKS:
- Git Repository: https://github.com/ReGaSLZR/Tatsu-Skills-and-Talents
- Google Drive folder for in-game footages and demo of how the project works: https://drive.google.com/drive/folders/1E-xKQyD9m7_PgHfvnrO8vfv5Df9CuKHB?usp=sharing
