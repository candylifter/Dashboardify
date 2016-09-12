import React from 'react';

class Navbar extends React.Component {
    render() {

        return (
         <nav className='navbar navbar-default'>
				<div className='container-fluid'>
					<div className='navbar-header'>
						<button type='button' className='navbar-toggle collapsed' data-toggle='collapse' data-target='#navbar-collapse' aria-expanded='false'>
							<span className='sr-only'>Toggle navigation</span>
							<span className='icon-bar'></span>
							<span className='icon-bar'></span>
							<span className='icon-bar'></span>
						</button>
						<a className='navbar-brand' href='#'>Dashboardify</a>
					</div>
					<div className='collapse navbar-collapse' id='navbar-collapse'>
						<ul className='nav navbar-nav navbar-right'>
							<li>
								<a href='#'>Home</a>
							</li>
							<li>
								<a href='#'>Settings</a>
							</li>
							<li>
								<a href='#'>Logout</a>
							</li>
						</ul>
					</div>
				</div>
			</nav>
        )

    }
}

export default Navbar;
