using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Scripts;
using TMPro;

public class Test_LauchScript
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator LaunchesProjectile()
    {
          // Arrange
        GameObject obj=new GameObject();
        LaunchScript launchScript = obj.AddComponent<LaunchScript>();
        var projectile = new GameObject();

        launchScript.timerUI=new GameObject().AddComponent<TextMeshProUGUI>();
        launchScript.projectile = projectile;
        var rigidbody = projectile.AddComponent<Rigidbody>();

        // Act
        launchScript.Launch();
        // Assert
        Assert.IsFalse(rigidbody.isKinematic);
        yield return null;
    }

    [UnityTest]
    public IEnumerator Test_GivePermissionToLaunch(){
         GameObject obj=new GameObject();
        LaunchScript launchScript = obj.AddComponent<LaunchScript>();
        var projectile = new GameObject();

        launchScript.timerUI=new GameObject().AddComponent<TextMeshProUGUI>();
        launchScript.projectile = projectile;
        var rigidbody = projectile.AddComponent<Rigidbody>();

        launchScript.GivePermissionToLaunch();

        Assert.AreEqual(launchScript.launchBall,true);

        yield return null;
    }
    
}