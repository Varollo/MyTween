# My Tween 
### What is it?
My Tween is my tweening library for unity.
### How to install it?
You can clone this repository or add it to your project with Unity's Package Manager if you have git installed on your machine.
### How to use it?
My Tween comes with a static class ```TweeningFunctions.cs``` full of tweening functions, which are adapted from this repository:
> https://github.com/danro/jquery-easing/blob/master/jquery.easing.js#L36

You can either use them as they are, or use the MyTweenEffect classes to facilitate your life, just create an instance of one the (currently) three built in classes that extend ```IMyTweenEffect```:
- ```MyTweenFloatEffect```
- ```MyTweenVector3Effect```
- ```MyTweenQuaternionEffect```

When you want to tween a value, just call the ```Execute()``` coroutine and read the result on the ```ResultValue``` propperty, like this:

```
tweenPosition = new MyTweenVector3Effect();
StartCoroutine(tweenPosition.ExecuteEffect(transform.position, Vector3.right, 2f));
transform.position = tweenPosition.ResultValue;
```
If you call the ```Execute()``` method while another tweening is being executed, it will not run until all the queued executions are complete.

You can access the queue by the propperty ```EffectQueue```.

### Disclaimer
I am not supporting this that much, it's just a thing for me to use in multiple projects, but none the less, you can use it if you don't mind the lack of support from my side.