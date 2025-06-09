
# Developer's Note

This game is currently inactive. I have migrated this to a newer version of Unity, and is additionally being redone in 3D instead of 2D. The source code of the newer version is and will not be publicly accessible (sorry)! However, this repo is being changed into something different. This will now instead have samples of different kinds of gameplay mechanics from the newer version, as well as some reasoning behind certain choices. Neon Rain is also a working title, not a finalized title. While this version is inactive, all old scripts are in the LEGACY folder! So all old code is going to be there. As ugly as it can be. The exception will be Editor tools which are in the Editor folder, as well as any packages which are in the Imports folder.

While the old code may not be pretty, it's nice to see sometimes how far we have come as developers! Additionally, I am unsure of how often this is going to be updated, but here and there - I will add more and more stuff.


## Table of Contents

- [Legacy Features](#legacy-features)
- [Samples](#samples)


## Legacy Features

These are all the features that I had implemented in the Legacy version, all of which can be seen in the Assets/Legacy folder.

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

These are all the features you can currently see that are sampled from the newer codebase. As well as each explanation behind certain decisions, and why. These can all be found in Assets/Samples

- [Game state](#game-state)
- [Extensions (Vector, GameObject, WaitForSeconds)](#extensions)
- [Object Pooling](#object-pooling)


## Documentation/Reasoning

Here is where you can view different pieces of code, and (potentially) why I made certain choices in my patterns.


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

