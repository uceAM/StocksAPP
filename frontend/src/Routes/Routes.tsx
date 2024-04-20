import React from 'react'
import { createBrowserRouter } from 'react-router-dom'
import HomePage from '../Pages/HomePage/HomePage'
import SearchPage from '../Pages/SearchPage/SearchPage'
import CompanyPage from '../Pages/CompanyPage/CompanyPage'
import App from '../App'
import CompanyProfile from '../Components/CompanyProfile/CompanyProfile'
import IncomeStatements from '../Components/IncomeStatements/IncomeStatements'
import DesignPage from '../Pages/DesignPage/DesignPage'

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
          element:<CompanyPage />,
          children:[{
            path:"company-profile",
            element:<CompanyProfile />
          },
          {
            path:"income-statements",
            element:<IncomeStatements />
          }]
        },
        {
          path:"design-page",
          element:<DesignPage />
        }
      ]
    }
  ])
  export default router;
