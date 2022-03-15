# Unity 3rd person Character Controller
![demo](repo-assets/demo.gif)

## What is this?
As the name implies, this is a 3rd person character controller built in Unity. More details will be provided throughout the course of development. The default controller (`BaseCharacterController`) uses a physics-based approach with rigidbodies, along with a capsule collider to move along the ground and slopes. This class is extendable, meaning you can create your own character controllers! For example, you could build a translation-based approach which inherits `BaseCharacterController`, and override the relevant methods.

### Controller states
The base controller is currently capable of being in three states:

- **Idle**: The character is stationary, playing an idle animation. From this state, they can either attack or walk.
- **Walk**: The character is walking around the environment, and can turn.
- **Attack**: The character plays an attack from the idle stance. Movement is not possible until the animation is complete.

### Inputs
The inputs for this small demo use the new `Unity.InputSystem` package, meaning it supports other input devices other than the keyboard & mouse. The demo is set up to two input schemes: gamepads and keyboard/mouse, however, it can be very easily extendable to support other input devices -- such as XR controllers. Simply add the devices in the `Player Input` component on the character.

## Getting started
To get started, simply clone this repo with Github Desktop or by running `git clone https://github.com/blewert/unity-char-controller.git`. You can also download the `.unitypackage` file from the *Releases* tab on this repository and importing it into your project.



## This project
Here are some details about this project.

### Version history
| Version/Tag | Date | Notes |
|---------|------|-------|
| `0.0.1` | `14/03/22` | First working skeleton project built in Unity. Includes sample character and animations. No movement.
| `0.1.0` | `15/03/22` | Basic physics-based third person controller, with fixed offset camera. Can handle slopes, and has attack, walk and idle states.

### Contributors

| Name | Username | Notes |
|------|-------|------|
| Benjamin Williams | `blewert` | Development


### Thanks
| Name | Description | 
|-------|--------|
| Adobe Mixamo | Sample character models & animations