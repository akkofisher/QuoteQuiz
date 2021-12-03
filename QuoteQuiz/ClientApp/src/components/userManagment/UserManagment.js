import React, { useState, Component } from 'react';
import { Modal, Button, Form } from "react-bootstrap";
import { useForm } from "react-hook-form";

export class UserManagment extends Component {
    static displayName = UserManagment.name;
    state = {
        isUserCreateModalOpen: false,
    };

    openUserCreateModal = () => this.setState({ isUserCreateModalOpen: true });
    closeUserCreateModal = () => this.setState({ isUserCreateModalOpen: false });

    createUserModel = { name: "wow" };

    constructor(props) {
        super(props);
        this.state = { usersData: [], loading: true };
    }

    componentDidMount() {
        this.getUsersData();
    }

    render() {

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : UserManagment.renderUsersTable(this.state.usersData);

        return (
            <div>
                <h1 id="tabelLabel">User Managment</h1>
                <>
                    <Button variant="primary" onClick={this.openUserCreateModal}>
                        Create User
                    </Button>

                    {/* <UserManagment.UserCreateModal
                        show={this.state.isUserCreateModalOpen} onHide={this.closeUserCreateModal}
                    /> */}

                    <Modal show={this.state.isUserCreateModalOpen} onHide={this.closeUserCreateModal}>
                        <Modal.Header closeButton>
                            <Modal.Title>Login Form</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <UserManagment.UserCreateForm2 onHide={() => this.createUserModel} onSubmitClick={(data) => this.createUser(data)} />
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={this.closeUserCreateModal}>
                                Cancel
                            </Button>
                        </Modal.Footer>
                    </Modal>
                </>
                {contents}
            </div>

        );
    }

    static UserCreateForm2 = (props) => {
        const { register, handleSubmit } = useForm();
        const onSubmit = (data) => {
            data.currentMode = parseInt(data.currentMode); // convert string to number (mode enum)
            props.onSubmitClick(data)
        };

        return (
            <Form onSubmit={handleSubmit(onSubmit)}>
                <Form.Control {...register("name", { required: true })} placeholder="name" />
                <Form.Control {...register("email", { required: true })} placeholder="email" />
                <select {...register("currentMode", { required: true })} className="form-control">
                    <option value="">Select Default User Mode</option>
                    <option value="1">Binary</option>
                    <option value="2">Multiple</option>
                </select>
                <Form.Group className="mb-3">
                    <Form.Check type="checkbox" {...register("isDisabled")} label="Disabled" />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Check type="checkbox" {...register("isDeleted")} label="Deleted" />
                </Form.Group>
                <Button variant="success" type="submit">
                    Create
                </Button>
            </Form>
        );
    };

    static UserCreateForm = ({ onSubmit }) => {
        const [name, setName] = useState("");
        const [email, setEmail] = useState("");
        return (
            <Form onSubmit={onSubmit}>
                <Form.Group controlId="formBasicName">
                    <Form.Label>Name</Form.Label>
                    <Form.Control
                        type="name"
                        placeholder="Enter name"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                </Form.Group>

                <Form.Group controlId="formBasicEmail">
                    <Form.Label>Email</Form.Label>
                    <Form.Control
                        type="email"
                        placeholder="Enter email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </Form.Group>


                <Form.Group controlId="formBasicCheckbox">
                    <Form.Check type="checkbox" label="Remember Me!" />
                </Form.Group>

                <Button variant="primary" type="submit">
                    Login
                </Button>
            </Form>
        );
    };

    static renderUsersTable(usersData) {

        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Current Mode</th>
                        <th>Deleted</th>
                        <th>Disabled</th>
                        <th>Create Date</th>
                        <th>Modified Date</th>
                    </tr>
                </thead>
                <tbody>
                    {usersData.map(user =>
                        <tr key={user.id}>
                            <td>{user.id}</td>
                            <td>{user.name}</td>
                            <td>{user.email}</td>
                            <td>{user.currentMode}</td>
                            <td>{user.isDeleted}</td>
                            <td>{user.isDisabled}</td>
                            <td>{user.createDate}</td>
                            <td>{user.lastModifiedDate}</td>
                        </tr>
                    )}
                </tbody>
            </table>

        );
    }

    static UserCreateModal(props) {
        let state = {
            name: "namea",
            email: "",
            currentMode: 1,
            isDeleted: false,
            isDisabled: false,
        }

        let handleChange = (e) => this.setState({ name: e.target.value })

        return (
            <Modal
                {...props}
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered
            >
                <Modal.Header closeButton>
                    <Modal.Title id="contained-modal-title-vcenter">
                        Create User
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group >
                        <Form.Label>Name: </Form.Label>
                        <Form.Control type="text" onChange={handleChange} value={state.name} placeholder="name" />
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button onClick={props.onHide}>Close</Button>
                </Modal.Footer>
            </Modal>
        );
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

    async createUser(data) {
       
        const response = await fetch('api/userManagment/CreateUser',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            }
        );
        const resultData = await response.json();
    

        if (resultData) {
            this.getUsersData();
            this.closeUserCreateModal();
        }

    }

}
