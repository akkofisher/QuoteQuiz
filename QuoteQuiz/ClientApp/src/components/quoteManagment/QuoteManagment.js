import React, { Component } from 'react';
import { Modal, Button, Form, Row } from "react-bootstrap";
import { useForm } from "react-hook-form";

export class QuoteManagment extends Component {
    static displayName = QuoteManagment.name;
    state = {
        isQuoteCreateModalOpen: false,
    };

    openQuoteCreateModal = () => this.setState({ isQuoteCreateModalOpen: true });
    closeQuoteCreateModal = () => this.setState({ isQuoteCreateModalOpen: false });

    createQuoteModel = { name: "wow" };

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

        return (
            <div>
                <h1>Quote Managment</h1>
                <Row>
                    <Button variant="primary" onClick={this.openQuoteCreateModal}>
                        Create Quote
                    </Button>

                    <Modal show={this.state.isQuoteCreateModalOpen} onHide={this.closeQuoteCreateModal}>
                        <Modal.Header closeButton>
                            <Modal.Title>Create Quote Form</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <QuoteManagment.QuoteCreateForm onHide={() => this.createQuoteModel} onSubmitClick={(data) => this.createQuote(data)} />
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

    static QuoteCreateForm = (props) => {
        const { register, handleSubmit } = useForm();
        const onSubmit = (data) => {
            data.mode = parseInt(data.mode); // convert string to number (mode enum)
            props.onSubmitClick(data)
        };

        let showModeElementsContent = <>123</>;

        const changeMode = (mode) => {
            
            if (mode === "Binary") {
                console.log(mode)
                showModeElementsContent = (
                    <div>
                        Binary
                    </div>
                );
            } else if (mode === "Multiple") {
                console.log(mode)
                showModeElementsContent = (
                    <div>
                        Multiple
                    </div>
                );
            }
        }

        return (
            <Form onSubmit={handleSubmit(onSubmit)}>
                <Form.Control {...register("quoteText", { required: true })} placeholder="Quote text" />

                <Form.Group className="mb-3">
                    <Form.Check type="radio" {...register("mode", { requared: true })} label="Binary" onChange={() => changeMode('Binary')} />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Check type="radio" {...register("mode", { requared: true })} label="Multiple" onChange={() => changeMode('Multiple')} />
                </Form.Group>

                {showModeElementsContent}

                <Form.Group className="mb-3">
                    <Form.Check type="checkbox" {...register("CorrectAnswer")} label="Is This Answer Correct?" />
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
                        <th>Deleted</th>
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
                            <td>{quote.isDeleted.toString()}</td>
                            <td>{quote.createDate}</td>
                            <td>{quote.lastModifiedDate}</td>
                        </tr>
                    )}
                </tbody>
            </table>

        );
    }

    static QuoteCreateModal(props) {
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

        const response = await fetch('api/quoteManagment/CreateQuoteBinary',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            }
        );
        const resultData = await response.json();


        if (resultData) {
            this.getQuotesData();
            this.closeQuoteCreateModal();
        }

    }

}
