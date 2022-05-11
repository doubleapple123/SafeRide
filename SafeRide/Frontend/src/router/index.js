import { createRouter, createWebHashHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import UserProtected from '../views/UserProtected.vue'
import UserRegistration from '../views/UserRegistration.vue'
import LoggedInInitialView from '../views/LoggedInInitialView.vue'
import AnalysisDashboard from '../views/AnalysisDashboard.vue'
import UserManagement from "../views/UserManagementPath";
import axios from "axios";
import RouteHistory from '@/components/RouteHistory.vue'
import SearchRoute from '@/components/SearchRoute.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/UserRegistration',
    name: 'UserRegistration',
    component: UserRegistration
  },
  {
    path: '/user',
    name: 'User',
    component: UserProtected
  },
  {
      path: '/map',
      name: 'Map',
      component: LoggedInInitialView
  },
  {
    path: "/usermanagement",
    name: "UserManage",
    component: UserManagement
  },
  {
    path: '/RouteHistory',
    name: 'History',
    component: RouteHistory
  },
  {
    path: '/SearchRoute',
    name: 'Search',
    component: SearchRoute
  }
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
