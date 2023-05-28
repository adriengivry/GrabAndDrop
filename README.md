# Grab And Drop: Documentation

## 1. Requirements
Grab And Drop (GAD) was developped with flexibility in mind, allowing you to integrate it into your games, no matter the type of controller, rendering pipeline, or Unity version you are using.
Setting up Grab And Drop may require some basic knowledge regarding:
    - Unity's Input System
    - Joint Physics
    - Scripting

## 2. Setup
Grab And Drop requires 2 scripts to operate.

### 2.1. GADInteractible
This script should be attached to any GameObject that can be grabbed. Any GameObject with a GADInteractible component MUST also have:
    - a Collider component
    - a Rigidbody component
Make sure your GameObject is properly setup before adding the GADInteractible component.

### 2.2. GADInteractor
The GADInteractor component should be added to your character GameObject. This component will search for any GameObject holding a GADInteractible component using the character's forward vector.
This component contains several public methods that will trigger the drag, drop, and throw actions:
    - Grab()
    - Drop()
    - Throw() 
You must setup your character controller to invoke these methods (ex. invoke Grab() when the 'E' key is pressed).
There are several ways to handle inputs in Unity, to learn more about it, check out Unity's documentation: https://docs.unity3d.com.
If you don't know how to extend your controller script to support Grab & Drop, you can use our GADInputEmitter, which does the work for you.
Do note that using the GADInputEmitter is not recommended, using a proper input handling solution is preferred.

### 2.3. GADInputEmitter
The GADInputEmitter is the easiest way to setup and test Grab And Drop. The GADInputEmitter is a very simple script that has been developped to work with both:
    - Unity new input system
    - Unity legacy input system

#### 2.3.1. Using the GADInputEmitter with Unity's new input system
If you are using Unity's new input system, you'll need to setup your input system so the behavior of the PlayerInput component is set to "Send Messages". The GADInputEmitter should be attached to the same GameObject as your PlayerInput component.
The GADInputEmitter needs a reference to the GADInteractor component you created previously. Make sure to set it up properly in the inspector.
Once done, you'll need to add 3 actions in your Input Settings asset:
    - Grab
    - Drop
    - Throw
Executing these actions will call the Grab(), Drop(), and Throw() methods of the referenced GADInteractor component.

#### 2.3.2. Using the GADInputEmitter with Unity's legacy input system
If you are using Unity's legacy input system, setting up the GADInputEmitter is very straightforward. Simply add this component to any GameObject in your scene, and fill in:
    - Interactor: a reference to the interactor component in your scene
    - Grab Key: the key to press to grab an object
    - Drop Key: the key to press to drop an object
    - Throw Key: the key to press to throw an object
Using the GADInputEmitter is fairly limiting, as only keyboard keys can be used to trigger the grab, drop, and throw actions. Using the same key for 2 different actions is also not supported.
Using your own input handling script is highly recommended.

## 3. Notes
Thank you for downloading Grab And Drop! We sincerely hope that this tool will help your games reach their full potential!
We would love to hear about your projects and what you think of this tool. Please leave us a review on the Unity Asset Store!
