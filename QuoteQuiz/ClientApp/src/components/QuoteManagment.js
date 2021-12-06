import React, { Component } from 'react';
import { Modal, Button, Form, Row, Col } from "react-bootstrap";
import { useForm } from "react-hook-form";
import toast from 'react-hot-toast';

export class QuoteManagment extends Component {
    static displayName = QuoteManagment.name;
    state = {
        isQuoteCreateModalOpen: false,
        creatQuoteMode: null,
    };

    openQuoteBinaryCreateModal = () => this.setState({ isQuoteCreateModalOpen: true, creatQuoteMode: "Binary" });
    openQuoteMultipleCreateModal = () => this.setState({ isQuoteCreateModalOpen: true, creatQuoteMode: "Multiple" });

    closeQuoteCreateModal = () => this.setState({ isQuoteCreateModalOpen: false });

    constructor(props) {
        super(props);
        this.state = { quotesData: [], loading: true };
    }

    componentDidMount() {
        this.getQuotesData();
    }

    render() {

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : QuoteManagment.renderQuotesTable(this.state.quotesData);

        const renderQuoteForm = () => {
            if (this.state.creatQuoteMode === "Binary") {
                return <QuoteManagment.QuoteBinaryCreateModal onSubmitClick={(data) => this.createQuote(data)} />;
            } else if (this.state.creatQuoteMode === "Multiple") {
                return <QuoteManagment.QuoteMultipleCreateModal onSubmitClick={(data) => this.createQuote(data)} />;
            } else {
                return <></>;
            }
        }

        return (
            <div>
                <h1>Quote Managment</h1>
                <Row md={4}>
                    <Col>
                        <Button variant="primary" onClick={this.openQuoteBinaryCreateModal} style={{ width: '18rem' }}>
                            Create Binary Quote
                        </Button>
                    </Col>
                    <Col>
                        <Button variant="primary" onClick={this.openQuoteMultipleCreateModal} style={{ width: '18rem' }}>
                            Create Multiple Quote
                        </Button>
                    </Col>
                </Row>
                <Row>
                    <Modal show={this.state.isQuoteCreateModalOpen} onHide={this.closeQuoteCreateModal}>
                        <Modal.Header closeButton>
                            <Modal.Title>Create Quote Form</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            {renderQuoteForm()}
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={this.closeQuoteCreateModal}>
                                Cancel
                            </Button>
                        </Modal.Footer>
                    </Modal>
                </Row>

                {contents}
            </div>

        );
    }

    static QuoteBinaryCreateModal = (props) => {
        const { register, handleSubmit } = useForm();
        const onSubmit = (data) => {
            data.mode = 1;
            props.onSubmitClick(data)
        };

        return (
            <Form onSubmit={handleSubmit(onSubmit)}>
                <Form.Control {...register("quoteText", { required: true })} placeholder="Quote text" />

                <Form.Group className="mb-3">
                    <Form.Check type="checkbox" {...register("CorrectAnswer")} label="Is This Answer Correct?" />
                </Form.Group>

                <Button variant="success" type="submit">
                    Create
                </Button>
            </Form>
        );

    };

    static QuoteMultipleCreateModal = (props) => {
        const { register, handleSubmit } = useForm();
        const onSubmit = (data) => {
            data.mode = 2;
            data.CorrectAnswerID = parseInt(data.CorrectAnswerID);
            props.onSubmitClick(data)
        };

        return (
            <Form onSubmit={handleSubmit(onSubmit)}>
                <Form.Control {...register("quoteText", { required: true })} placeholder="Quote text" />

                <Form.Control {...register("MultiplePossibleAnswers.0.PossibleAnwerText", { required: true })} placeholder="Possible Answer Text A" />
                <Form.Group className="mb-3">
                    <Form.Check type="radio" value="1" {...register("CorrectAnswerID", { required: true })} label="Is This Answer Correct?" />
                </Form.Group>

                <Form.Control {...register("MultiplePossibleAnswers.1.PossibleAnwerText", { required: true })} placeholder="Possible Answer Text B" />
                <Form.Group className="mb-3">
                    <Form.Check type="radio" value="2" {...register("CorrectAnswerID", { required: true })} label="Is This Answer Correct?" />
                </Form.Group>

                <Form.Control {...register("MultiplePossibleAnswers.2.PossibleAnwerText", { required: true })} placeholder="Possible Answer Text C" />
                <Form.Group className="mb-3">
                    <Form.Check type="radio" value="3" {...register("CorrectAnswerID", { required: true })} label="Is This Answer Correct?" />
                </Form.Group>

                <Button variant="success" type="submit">
                    Create
                </Button>
            </Form>
        );

    };

    static renderQuotesTable(quotesData) {

        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>quoteText</th>
                        <th>Current Mode</th>
                        <th>Create Date</th>
                        <th>Modified Date</th>
                    </tr>
                </thead>
                <tbody>
                    {quotesData.map(quote =>
                        <tr key={quote.id}>
                            <td>{quote.id}</td>
                            <td>{quote.quoteText}</td>
                            <td>{quote.mode}</td>
                            <td>{quote.createDate}</td>
                            <td>{quote.lastModifiedDate}</td>
                        </tr>
                    )}
                </tbody>
            </table>

        );
    }

    static QuoteBinaryCreateModal(props) {
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
                        Create Quote
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

    async getQuotesData() {
        const response = await fetch('api/quoteManagment/GetQuotes',
            {
                method: "GET",
                headers: { 'Content-Type': 'application/json' },
            }
        );
        const resultData = await response.json();
        this.setState({ quotesData: resultData, loading: false });
    }

    async createQuote(data) {
        let resultData = null;

        if (data.mode === 1) {
            const response = await fetch('api/quoteManagment/CreateQuoteBinary',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(data)
                }
            );
            resultData = await response.json()
        } else if (data.mode === 2) {
            const response = await fetch('api/quoteManagment/CreateQuoteMultiple',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(data)
                }
            );
            resultData = await response.json()
        }

        if (resultData) {
            this.getQuotesData();
            this.closeQuoteCreateModal();
            toast.success('Successfully quote added!');
        }else{
            toast.error('This is an error!');
        }


    }

}
