<template>
  <div id="nav">
    <router-link to="/">Home</router-link>
    <br />
    <router-link to="/login">Login</router-link>
    <br />
    <router-link to="/UserRegistration">User Registration</router-link>
    <br />
    <router-link to="/User">User</router-link>
    <br />
    <router-link to="/Map">Default Map</router-link>
    <br />
    <router-link to="/Analytics">Analytics Dashboard</router-link>
    <br />
    <router-link to="/RouteHistory">Routes History</router-link>
    <br />
    <router-link to="/SearchRoute">Search Route</router-link>
  </div>
  <router-view/>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
