import React from 'react'

import { Card, CardActions, CardHeader, CardText } from 'material-ui/Card'
import Divider from 'material-ui/Divider'

import { LoginForm } from 'components'

class Login extends React.Component {
  render () {
    const style = {
      width: '100%',
      height: 'calc(100vh - 64px)',
      backgroundImage: 'url(\'/static/login-bg.jpg\')',
      backgroundPosition: 'center',
      backgroundSize: 'cover',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      card: {
        width: '25em'
      }
    }

    return (
      <div style={style}>
        <Card style={style.card}>
          <CardHeader
            title='Without Avatar'
            subtitle='Subtitle'
            actAsExpander
            showExpandableButton
          />
          <LoginForm />
          <Divider />
        </Card>
      </div>
    )
  }
}

export default Login

// <div className='container'>
//   <div className='page-header text-center'>
//     <h1>Login to Dashboardify</h1>
//   </div>
//   <div className='row'>
//     <div className='col-sm-12 col-md-6 col-md-offset-3 col-lg-4 col-lg-offset-4'>
//       <LoginForm />
//     </div>
//   </div>
// </div>
