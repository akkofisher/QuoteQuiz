import React, { Component } from 'react';
import { Button, Form, Row, Card } from "react-bootstrap";
import { useForm } from "react-hook-form";
import authService from "./api-authorization/AuthorizeService"
import toast from 'react-hot-toast';

export class UserAuthSettings extends Component {
    static displayName = UserAuthSettings.name;
    state = {
        isUserCreateModalOpen: false,
        ready: false,
        isAuthenticated: false,
        userName: null
    };

    constructor(props) {
        super(props);
        this.state = { usersData: [], loading: true };
    }

    async componentDidMount() {
        this.getUsersData();
        const isAuthenticated = await authService.isAuthenticated();
        this.setState({ ready: true, isAuthenticated });
    }

    render() {
        const userLogOutClick = () => { this.userLogOut() };

        let authenticatedView = this.state.isAuthenticated
            ? <Row>
                <UserAuthSettings.UserChangeModeForm onSubmitClick={(data) => this.updateUserMode(data)} />
                <Button onClick={userLogOutClick}>Log Out</Button>
            </Row>
            :
            <UserAuthSettings.UserCreateForm usersData={this.state.usersData} onSubmitClick={(data) => this.userAuth(data)} />;

        if (!this.state.ready) {
            return <div></div>;
        } else {
            return (
                <Row>
                    <h1>User Auth & Settings</h1>
                    <Card style={{ width: '23rem' }}>
                        <Card.Body>
                            <Card.Title className="mb-1 ">
                                {authenticatedView}
                            </Card.Title>
                        </Card.Body>
                    </Card>
                </Row>
            );
        }
    }


    static UserCreateForm = (props) => {

        const { register, handleSubmit } = useForm();
        const onSubmit = (data) => {
            data.userID = parseInt(data.userID);
            props.onSubmitClick(data)
        };
        return (
            <Form onSubmit={handleSubmit(onSubmit)} style={{ width: '18rem' }}>
                <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label>User</Form.Label>
                    <select {...register("userID", { required: true })} className="form-control">
                        <option value="">Select User</option>
                        {props.usersData.map(user => <option key={user.id} value={user.id}>{user.name}</option>)}
                    </select>
                </Form.Group>

                <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="*******" />
                </Form.Group>

                <Button variant="success" type="submit">
                    Authorize
                </Button>
            </Form>
        );
    };

    static UserChangeModeForm = (props) => {
        const { register, handleSubmit } = useForm();
        const onSubmit = (data) => {
            data.userMode = parseInt(data.userMode);
            props.onSubmitClick(data)
        };

        return (
            <Form onSubmit={handleSubmit(onSubmit)} style={{ width: '18rem' }}>
                <select {...register("userMode", { required: true })} className="form-control">
                    <option value="">Select Default User Mode</option>
                    <option value="1">Binary</option>
                    <option value="2">Multiple</option>
                </select>

                <Button variant="success" type="submit">
                    Chage Mode
                </Button>
            </Form>
        );
    };

    async userAuth(data) {
        await authService.userSimpleAuth(data)
        const isAuthenticated = await authService.isAuthenticated();
        this.setState({ ready: true, isAuthenticated });
    }

    async userLogOut() {
        const isAuthenticated = await authService.userLogOut();
        this.setState({ ready: true, isAuthenticated });
    }

    async getUsersData() {
        const response = await fetch('api/userManagment/GetUsers',
            {
                method: "GET",
                headers: { 'Content-Type': 'application/json' },
            }
        );
        const resultData = await response.json();
        this.setState({ usersData: resultData, loading: false });
    }

    async updateUserMode(data) {
        const response = await fetch(`api/UserQuote/UpdateUserMode?userMode=${data.userMode}`,
            {
                method: "GET",
                headers: { 'Content-Type': 'application/json' },
            }
        );

        const resultData = await response.json();
        if (resultData) {
            toast.success('Successfully user added!');
        } else {
            toast.error('This is an error!');
        }
    }


}
