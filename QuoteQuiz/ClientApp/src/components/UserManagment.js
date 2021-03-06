import React, { Component } from 'react';
import { Modal, Button, Form, Row, Col, ProgressBar } from "react-bootstrap";
import { useForm } from "react-hook-form";
import toast from 'react-hot-toast';

export class UserManagment extends Component {
    static displayName = UserManagment.name;
    state = {
        isUserCreateModalOpen: false,
        isUserReviewModalOpen: false,
    };

    openUserCreateModal = () => this.setState({ isUserCreateModalOpen: true });
    closeUserCreateModal = () => this.setState({ isUserCreateModalOpen: false });

    openUserReviewModal = (userID) => {
        this.setState({ isUserReviewModalOpen: true })
        this.getReviewUserData(userID);
    };
    closeUserReviewModal = () => this.setState({ isUserReviewModalOpen: false });

    constructor(props) {
        super(props);
        this.state = {
            usersData: [],
            loading: true,
            loadingUserReview: true,
            userReviewData: {},
        };
    }

    componentDidMount() {
        this.getUsersData();
    }

    render() {

        let userReviewContent = this.state.loadingUserReview
            ? <p><em>Loading...</em></p>
            : UserManagment.renderUsersReview(this.state.userReviewData);

        return (
            <div>
                <h1>User Managment</h1>
                <Row md={4}>
                    <Col>
                        <Button variant="primary" onClick={this.openUserCreateModal} style={{ width: '18rem' }}>
                            Create User
                        </Button>
                    </Col>
                </Row>
                <Row>
                    <Modal show={this.state.isUserCreateModalOpen} onHide={this.closeUserCreateModal}>
                        <Modal.Header closeButton>
                            <Modal.Title>Create User Form</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <UserManagment.UserCreateForm onSubmitClick={(data) => this.createUser(data)} />
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={this.closeUserCreateModal}>
                                Cancel
                            </Button>
                        </Modal.Footer>
                    </Modal>

                    <Modal show={this.state.isUserReviewModalOpen} onHide={this.closeUserReviewModal}>
                        <Modal.Header closeButton>
                            <Modal.Title>User Review</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            {userReviewContent}
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={this.closeUserReviewModal}>
                                Close
                            </Button>
                        </Modal.Footer>
                    </Modal>
                </Row>

                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Email</th>
                            <th>Current Mode</th>
                            <th>Disabled</th>
                            <th>Create Date</th>
                            <th>Modified Date</th>
                            <th>Review</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.usersData.map(user =>
                            <tr key={user.id}>
                                <td>{user.id}</td>
                                <td>{user.name}</td>
                                <td>{user.email}</td>
                                <td>{user.currentMode}</td>
                                <td>{user.isDisabled.toString()}</td>
                                <td>{user.createDate}</td>
                                <td>{user.lastModifiedDate}</td>
                                <td>
                                    <Button variant="info" onClick={() => this.openUserReviewModal(user.id)} style={{ width: '5rem' }}>
                                        View
                                    </Button>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>

        );
    }

    static renderUsersReview(userReviewData) {
        const userCorrectAnswerPercentage = userReviewData.userCorrectAnswerPercentage;

        const progressInstance = <ProgressBar now={userCorrectAnswerPercentage} label={`${userCorrectAnswerPercentage}%`} />;
        return (
            <>
                <Col>
                    <label>Name: {userReviewData.userName}</label>
                </Col>
                <Col>
                    <label>Total Answer Questions: {userReviewData.totalUserAnsweredQuestions}</label>
                </Col>
                <Col>
                    {progressInstance}
                </Col>
            </>
        );
    }

    static UserCreateForm = (props) => {
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

                <Button variant="success" type="submit">
                    Create
                </Button>
            </Form>
        );
    };

    static UserCreateModal(props) {
        let state = {
            name: "namea",
            email: "",
            currentMode: 1,
            isDisabled: false,
        }

        let handleChange = (e) => this.setState({ name: e.target.value })

        return (
            <Modal
                {...props}
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered>

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

    async getReviewUserData(userID) {
        this.setState({ userReviewData: {}, loadingUserReview: true });
        const response = await fetch(`api/userManagment/ReviewUser?userID=${userID}`,
            {
                method: "GET",
                headers: { 'Content-Type': 'application/json' },
            }
        );
        const resultData = await response.json();
        this.setState({ userReviewData: resultData, loadingUserReview: false });
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
            toast.success('Successfully changed!');
            this.getUsersData();
            this.closeUserCreateModal();
        } else {
            toast.error('This is an error!');
        }

    }

}
