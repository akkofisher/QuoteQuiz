import React, { Component } from 'react';
import { Button, Form, Row } from "react-bootstrap";
import { useForm } from "react-hook-form";

export class UserAuthSettings extends Component {
    static displayName = UserAuthSettings.name;
    state = {
        isUserCreateModalOpen: false,
    };

    constructor(props) {
        super(props);
        this.state = { usersData: [], loading: true };
    }

    componentDidMount() {
        this.getUsersData();
    }

    render() {
        return (
            <Row>
                <h1>User Auth & Settings</h1>
                <Row>
                    <UserAuthSettings.UserCreateForm usersData={this.state.usersData} onSubmitClick={(data) => this.userSimpleAuth(data)} />
                </Row>
            </Row>

        );
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

    async userSimpleAuth(data) {

        const response = await fetch('api/userManagment/UserSimpleAuth',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data.userID),
            }
        );
        const resultData = await response.json();


        if (resultData) {
        }

    }

}
