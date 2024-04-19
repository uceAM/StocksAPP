import React from 'react'
import { createBrowserRouter } from 'react-router-dom'
import HomePage from '../Pages/HomePage/HomePage'
import SearchPage from '../Pages/SearchPage/SearchPage'
import CompanyPage from '../Pages/CompanyPage/CompanyPage'
import App from '../App'

  const router = createBrowserRouter([
    {
      path:"/",
      element:<App />,
      children:[
        {
          path:"",
          element:<HomePage />
        },
        {
          path:"search",
          element:<SearchPage />
        },
        {
          path:"company/:ticker",
          element:<CompanyPage />
        }
      ]
    }
  ])
  export default router;
