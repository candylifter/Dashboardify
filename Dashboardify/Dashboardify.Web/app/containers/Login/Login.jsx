import React, {PropTypes} from 'react'

import { LoginForm } from 'components';

class Login extends React.Component {
	render() {
		return (
			<div className="container">
				<div className="page-header text-center">
				  <h1>Login to Dashboardify</h1>
				</div>
				<div className="row">
					<div className="col-sm-12 col-md-6 col-md-offset-3 col-lg-4 col-lg-offset-4">
						<LoginForm/>
					</div>
				</div>
			</div>
		)
	}
}

export default Login;
