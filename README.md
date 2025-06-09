# Introduction

> Neon Rain is a 2D top-down action/rougelike shooter game built using the Unity game engine and C#. 

This game is a passion project of mine, as well as a huge learning experience. It helped me to better understand coding patterns, better ways of tackling problems, as well as better strategies (beyond just programming patterns) to face certain challenges. Additionally, I learned a lot in being able to break down a complex feature into code - and further how to make that code both more readable, and more scalable/modular.



## Table of Contents

- [Developer's Note (please read!)](#developers-note)
- [Legacy Features](#legacy-features)
- [Samples](#samples)
- [Lessons Learned](#lessons-learned)



## Developer's Note

This version of the game is currently inactive. I have migrated this to a newer version of Unity, and is additionally being redone in 3D instead of 2D. The source code of the newer version is, and will not be publicly accessible (sorry)! However, this repo is being changed into something different. This will now instead have samples [(in Assets/Samples)](/Assets/Samples/) of different kinds of gameplay mechanics from the newer version, as well as some reasoning behind certain design choices. Neon Rain is also a working title, not a finalized title and the newer version of this will likely be different. 

While this version is inactive, all old scripts are in the [LEGACY folder](/Assets/LEGACY/)! So all old code is going to be there. As ugly as it can be. The exception will be Editor tools which are in [Assets/Editor](/Assets/Editor/), as well as any packages which are in [Assets/Imports](/Assets/Imports/).

While the old code may not be pretty, it's nice to see sometimes how far we have come as developers! Additionally, I am unsure of how often this is going to be updated, but here and there - I will add more and more stuff.



## Legacy Features

These are all the features that had been implemented in the most current state of the game. (v0.501102a)

- Stats (basic implementation)
- Main menu
- SFX (wow!)
- Combat (shooting/frontal cone swipe, health, etc.)
- Leveling/XP
- Upgrade System
- Abilities
- Currency System
- Loot System (weapon drops, that can be picked up)
- Weapons (different stats/kinds)
- Corruption System (kiss/curse combat system)
- Object pooling
- Enemy spawning
- Difficulty scaling with player level
- Game state (start countdown, active, game over)
- End game stats
- Editor tooling (weapon generation)



## Samples

These are going to the code samples from the newer codebase of this game (which is not publicly accessible) that I have taken and put here. This will generally just cover things that I use across all games that I have developed/am developing.

These are the samples I have currently put into this codebase:

- [Game State/State Machines](#game-state)
- [Extensions (Vector, GameObject, WaitForSeconds)](#extensions)
- [Object Pooling](#object-pooling)



## Documentation/Reasoning

Here is where you can view different pieces of code, and (potentially) why I made certain choices and design decisions.


### Game State

[Click here to see implementation of these classes.](#game-state-usage)

Here you can find the StateMachine, and StateNode classes.
As far as most things go, yes I very much could go the composition route instead of inheritance. While this is not a specific reason for why I am using inheritance, instead of interfacts - I do prefer the way this looks overall.

I do not necessarily like using virtual functions over abstract functions. Calling base.WhateverFunction() can be quite annoying, however in this scenario, I know at all times I am going to be doing some basic logic within the ChangeState function, as well as the StateUpdate function. I could potentially just make these non-abstract/virtual, however in the off-chance I would like to add some additional functionality in a specific implementation of the StateMachine class, I like having that option. My preference overall is using abstract functions, as it forces me to remember to actually implement them!

While I know I could just make a  couple interfaces like IStateMachine and IStateNode, and implement the same logic - I simply just chose to go this route. And at the moment, for my use cases, I do not see this becoming an issue in the future.

#### State Machine

A base state machine class. 

```csharp
namespace Samples.State
{
    public abstract class StateMachine<T> where T : StateMachine<T>
    {
        public StateNode<T> currentState { get; private set; }

        public abstract void InitializeStateMachine();

        public virtual void ChangeState(StateNode<T> newState){
            currentState?.OnStateExit((T)this);
            currentState = newState;
            currentState?.OnStateEnter((T)this);
        }

        public virtual void StateUpdate(){
            currentState?.OnStateUpdate((T)this);
        }
    }
}
```


#### State Node
A base state node class.
```csharp
namespace Samples.State
{
    public abstract class StateNode<T>
    {
        public abstract void OnStateEnter(T stateMachine);
        public abstract void OnStateUpdate(T stateMachine);
        public abstract void OnStateExit(T stateMachine);
    }
}
```

#### Game State Usage

Here is an example of using these classes - and how I've used them.


**Game State Machine**

This is a basic Game State Machine I had used to control the flow of the game, and trigger certain events during gameplay.


```csharp
using System;
using Samples.State;
using UnityEngine;

namespace Samples.State
{
    public class GameStateMachine : StateMachine<GameStateMachine>
    {
        [Header("State Nodes")]
        public GeneratingState generatingState;
        public CountdownState countdownState;
        public ActiveState activeState;
        public GameOverState gameOverState;

        public event Action<StateNode<GameStateMachine>> OnGameStateChanged;

        public float CountdownTimer = 0f;
        protected GameSettings settings;

        public GameStateMachine(GameSettings settings)
        {
            this.settings = settings;
            CountdownTimer = settings.CountdownTime;
            InitializeStateMachine();
        }

        public override void ChangeState(StateNode<GameStateMachine> newState)
        {
            base.ChangeState(newState);
            OnGameStateChanged?.Invoke(newState);
        }

        public override void InitializeStateMachine()
        {
            generatingState = new GeneratingState();
            countdownState = new CountdownState();
            activeState = new ActiveState();
            gameOverState = new GameOverState();

            ChangeState(generatingState);
        }

        public void StartCountdown()
        {
            ChangeState(countdownState);
        }

        public void StartGame()
        {
            ChangeState(activeState);
        }

        public void TriggerGameOver() {
            ChangeState(gameOverState);
        }

        public void RestartGame() {
            ChangeState(countdownState);
        }
    }
}
```


**Countdown State**

This is a basic CountdownState node. This simply counts down the time, then will trigger the StartGame function within the GameStateMachine class when the timer reaches 0.

```csharp
using UnityEngine;

namespace Samples.State
{
    public class CountdownState : StateNode<GameStateMachine>
    {
        private float countdownTimer;
        private float currentTimer;

        public override void OnStateEnter(GameStateMachine stateMachine)
        {
            Debug.Log($"<color=green>GAME STATE: </color>Entered COUNTDOWN state");
            countdownTimer = stateMachine.CountdownTimer;
            currentTimer = countdownTimer;
        }

        public override void OnStateExit(GameStateMachine stateMachine)
        {
            Debug.Log($"<color=green>GAME STATE: </color>Exiting COUNTDOWN state");
        }

        public override void OnStateUpdate(GameStateMachine stateMachine)
        {
            currentTimer -= Time.deltaTime;

            if (currentTimer <= 0f)
            {
                stateMachine.StartGame();
            }
        }
    }
}
```


### Extensions

Here are some basic utility functions/extensions. They are decently self-explanatory on why I would use them. These are pretty commonly used, and things I generally have across all projects. There won't be really any explanation on these/why. These are just useful!

#### GameObject Extensions

GetOrAdd is generally useful to ensure that the component we are trying to access wil in fact exist. For the most part, this is used for specific things that **NEED** to exist. As an example, Entities will always need a Health class. Becuase in some way, shape or form, I am going to need to access the health for that Entity, and Entities (by definition of my game), is something that will have Health. We often create tons of prefabs for enemies, and these are all their own GameObjects, so there is always the possibility I will simply forget to add the Health.cs script onto that prefab. Yes, I can do null-checking when calling any health-related functions within the Entity class, however I could also just simply add the component, if it is not there. Because I will need to also initialize that Health system with lots of data, so I need to have that on there.

Is this the only reason I use it? No, and that may not exactly be the best use-case for this. And there are going to be many different times in which I need to 100% have a different component on this script. This also helps with the potential of possibly doing something silly like adding that component multiple times in different scripts on the same object. Sure [DisallowMultipleComponent] works, as well as null checking and only adding if it's not there, but this is the same effectively, and easier to type!

```csharp
using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    /// Get the component attached to a GameObject, and if it doesn't exist - add it.
    /// </summary>
    /// <typeparam name="T">The component we want</typeparam>
    /// <param name="gameObject">The GameObject we are accessing</param>
    /// <returns></returns>
    public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (!component) component = gameObject.AddComponent<T>();

        return component;
    }
}
```

#### Vector Extensions

ZeroAxis is super useful in the 3D version of this game. As an example, I can use the mouse's position to aim the character. As this is a top-down game, I do not want the player to also point down to the ground my mouse is actually hitting, only in the direction of it. So I will ZeroAxis the Y axis. This actually is used fairly often. As well, I could simply make each into their own function. Like a ZeroY, ZeroX, and ZeroZ function. This will probably work better in the future - and may be a likely change later on!

RandomizeVector is pretty common just to get some variance on certain things. I.e: I create some DamageText when damaging an enemy, and I never really want that to spawn and appear in the exact same position every single time. It just gets a little tiring. There is an additional function I've created for transforms that is just called RandomizePosition, which really just calls RandomizeVector on transform.position. These accomplish similar tasks, just easier to write transform.RandomizePosition, rather than transform.position.RandomizeVector every time!

```csharp
using UnityEngine;

public enum Axis { X, Y, Z }

public static class VectorExtensions
{
    
    /// <summary>
    /// Zero out a certain axis on a given Vector3
    /// </summary>
    /// <param name="vector">Vector that we want to zero an axis of</param>
    /// <param name="axis">Which axis we want to be zeroed out</param>
    /// <returns></returns>
    public static Vector3 ZeroAxis(this Vector3 vector, Axis axis){
        switch(axis){
            case Axis.X:
                return new Vector3(0f, vector.y, vector.z);
            case Axis.Y:
                return new Vector3(vector.x, 0f, vector.z);
            case Axis.Z:
                return new Vector3(vector.x, vector.y, 0f);
            default:
                return new Vector3(vector.x, vector.y, vector.z);
        }
    }

    /// <summary>
    /// Randomize a Vector3's X, Y, Z values
    /// </summary>
    /// <param name="vector3">The original Vector3</param>
    /// <param name="randomVector">The random values we'd like to use to randomize</param>
    /// <returns></returns>
    public static Vector3 RandomizeVector(this Vector3 vector3, Vector3 randomVector){
        return vector3 + new Vector3(
            Random.Range(-randomVector.x, randomVector.x),
            Random.Range(-randomVector.y, randomVector.y),
            Random.Range(-randomVector.z, randomVector.z)
        );
    }
}
```

#### WaitForSeconds Extensions

```csharp
/*
    This is actually not an original thing that I myself created, this came from a channel on YouTube: git-amend
    Link: https://www.youtube.com/@git-amend
*/

using System.Collections.Generic;
using UnityEngine;

public static class WaitForSecondsExtensions
{
    private static Dictionary<float, WaitForSeconds> dictionary = new Dictionary<float, WaitForSeconds>();

    /// <summary>
    /// Get or create a cached WaitForSeconds
    /// </summary>
    /// <param name="speed">The value of WaitForSeconds we need</param>
    /// <returns></returns>
    public static WaitForSeconds GetWaitForSeconds(float speed)
    {
        if (dictionary.TryGetValue(speed, out WaitForSeconds wfs)) return wfs;
        else
        {
            WaitForSeconds _wfs = new WaitForSeconds(speed);
            dictionary.Add(speed, _wfs);
            return _wfs;
        }
    }
}
```


### Object Pooling

This was a hard task. I wanted something decently comprehensive and custom for pooling objects to save memory. I wanted it to be decently versatile and pretty fast - so I created a custom PooledObject script. This will accomplish many things that I will need within an object pool. I will likely add some more explanation here eventually - just be sure to check out the file at the moment if you'd like to see it, as it is pretty well commented!

[Click here to view the PooledObject.cs file.](/Assets/Samples/ObjectPooling/PooledObject.cs)



## Lessons Learned

I have learned tons of things since starting my development journey. One of the bigger ones I believe, is how to go from gameplay idea/feature -> code. I always remember when I was younger, watchings tons and tons of YouTube tutorials, and reading tons of forum posts on how to implement a specific gameplay mechanic. As an example: health/combat. However, what many tutorials will do, is give you a fairly basic implementation of that. Where I click my mouse, it will shoot a projectile, and then that projectile will subtract health from whatever I hit. But let's say I want to go beyond that, and also calculate how far I was from the enemy, or let's say I want to shoot multiple projectiles in different directions, or let's say I want to do more than just subtract health and also additionally add some kind of poison effect to the enemy. The list can go on and on and on. 

The issue really starts arising when we start trying to create something more complex than what the first tutorial taught me, and then I go watch another tutorial on the new things I want to add, and their version of the Health system is completely different! Then I need to go and change my Health system to match theirs.

Beyond just being able to break down a problem, this project was a big learning experience and challenge when it comes to decoupling code and making things more modular. The biggest thing I learned, was that I needed to be consistent. In the Legacy code, you can probably easily find multiple instances where I am using event-driven logic, and then also not using it when I could. User Interface related events is where I became pretty inconsistent. In some areas, I just call invoke an event, but then also access a static Instance of another class to also do some kind of logic instead of simply just subscribing to that event across all my classes. 

An example would be something like this.

In my [EnemyBase](/Assets/LEGACY/_Scripts/Enemies/EnemyBase.cs) class I do this logic:
```csharp
    void Die(){
        if(Extensions.Roll100(_dropChance)){
            LootManager.Instance.DropLoot(transform.position, _baseLuck);
        }
        Inventory.Instance.AddGold(_goldDrop);
        LevelSystem.Instance.AddExperience(Mathf.RoundToInt(enemyData.xpAmount * _lvlScaler.EnemyXPDropModifier));
        CorruptionManager.Instance.IncreaseCorruptionAmount(enemyData.corruptionDrop);
        // CorruptionManager.Instance.AddCorruption(_corruptionDrop);
        ScoreManager.scoreManager.AddToScore(_scoreAmnt);
        GameStats.enemiesKilled++;
        GameStats.currentAmountOfEnemies--;
        Destroy(gameObject);
    }
```

And in my [HealthSystem](/Assets/LEGACY/_Scripts/Combat/HealthSystem.cs) class I do this logic:
```csharp
    public void DecreaseCurrentHealth(float amount, bool crit){
        CurrentHealth -= Mathf.RoundToInt(amount);
        onDamage?.Invoke(amount, crit);
        if(CurrentHealth <= 0){
            CurrentHealth = 0;
            ChangeHealthBarValue(0);
            onDeath?.Invoke();
        }
        else{
            ChangeHealthBarValue(CurrentHealth);
        }
    }
```

In my EnemyBase class, I have a ScriptableObject which contains a lot of these values, and I should just realistically invoke an event called something like OnEnemyDeath, which would pass that ScriptableObject through, and then all of those classes can subscribe to that event. But instead, I was accessing a bunch of static Instances and triggering a function manually - which is both tedious, and not great. As soon as I start addding more and more things I want to happen, I have to go to me EnemyBase class, access that class I need in some way, call that function and make sure I'm passing the right arguments through. It's both not safe, and not consistent. Because I'm invoking an event in the HealthSystem class, which my EnemyBase subscribes to, and then instead of just using that event - I am calling more logic. 

Not only that, but in the HealthSystem class I invoke an event onDamage in the DecreaseCurrentHealthFunction. Instead of having my UI subscribe to that event, I have a direct reference to the healthbar, in which I then change that value in the ChangeHealthbarValue function. 

Again, it's really all about being consistent. As this was a pretty early on project for me - I was inconsistent in a lot of areas, causing a lot of nightmares and headaches that could have been easily avoided if I just followed certain patterns. 

There are many more lessons that I have learned. I think this section is just getting a little long winded and repetitive. So this is where I'll stop.