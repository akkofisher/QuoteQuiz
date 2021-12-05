import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import authService from './api-authorization/AuthorizeService'

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
            isAuthenticated: false,
            userName: null
        };
    }

    async componentDidMount() {
        const isAuthenticated = await authService.isAuthenticated();
        this.setState({ isAuthenticated });
    }


    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    render() {
        // let authenticatedView = this.state.isAuthenticated
        //     ? this.renderAuthenticatedView() : null;

        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/">QuoteQuiz</NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/user-settings">User Auth & Settings</NavLink>
                                </NavItem>
                                {/* {authenticatedView} */}
                                {this.renderAuthenticatedView()}
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/user-managment">User managment</NavLink>
                                </NavItem>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/quote-managment">Quote managment</NavLink>
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }

    renderAuthenticatedView() {
        return (
            <NavItem>
                <NavLink tag={Link} className="text-dark" to="/user-quote">User Quote</NavLink>
            </NavItem>
        );
    }
}
