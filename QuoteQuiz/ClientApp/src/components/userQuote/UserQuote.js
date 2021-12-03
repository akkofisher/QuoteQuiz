import React, { Component } from 'react';
import { Button, Form, Card, Col, Row, Badge } from "react-bootstrap";
import { useForm } from "react-hook-form";

export class UserQuote extends Component {
    static displayName = UserQuote.name;

    constructor(props) {
        super(props);
        this.state = {
            userQuoteData: [],
            loading: true,
            showAnswer: false,
            answerResult: {},
            showNoQuotes: false
        };
    }

    componentDidMount() {
        // this.getUsersData();
        this.getUserQuote({ userID: 1 });
    }

    render() {

        // let contents = this.state.loading
        //     ? <p><em>Loading...</em></p>
        //     : UserQuote.renderUsersTable(this.state.usersData);

        let userQuoteContent = this.state.loading
            ? <p><em>Loading...</em></p>
            : (!this.state.showAnswer && !this.state.showNoQuotes ? this.renderUserQuoteCard(this.state.userQuoteData) : null);

        let showAnswerContent = this.state.showAnswer
            ? this.renderShowAnswer(this.state.answerResult) : null;

        let showNoQuotesContent = this.state.showNoQuotes
            ? this.renderShowNoQuotes() : null;

        return (
            <div>
                <h1>User Quote</h1>
                <Row>
                    <Col md="auto">
                        {userQuoteContent}
                    </Col>
                </Row>
                <Row>
                    {showAnswerContent}
                </Row>
                <Row>
                    {showNoQuotesContent}
                </Row>
            </div>

        );
    }

    renderShowAnswer(answerResult) {
        return (
            <Row>
                <Row>
                    Your Answer is: {answerResult.isCorrectAnswered.toString()}
                </Row>
                <Row>
                    <Badge pill bg="success">
                        {answerResult.correctAnswerText}
                    </Badge>
                </Row>
                <Row>
                    <Button variant="primary" onClick={() => this.getUserQuote({ userID: 1 })}>Show Next Quote</Button>
                </Row>
            </Row>
        );
    }

    renderShowNoQuotes() {
        return (
            <Badge pill bg="info">
                No More Quotes
            </Badge>
        )
    }

    renderUserQuoteCard(userQuoteData) {
        if (userQuoteData.mode === 1) {
            return <UserQuote.UserBinaryQuoteCard userQuoteData={userQuoteData} onSubmitClick={(data) => this.answerUserQuote(data)} />;
        } else if (userQuoteData.mode === 2) {
            return <UserQuote.UserMultipleQuoteCard userQuoteData={userQuoteData} onSubmitClick={(data) => this.answerUserQuote(data)} />;
        }
        return null;
    }


    static UserBinaryQuoteCard = (props) => {
        const { register, handleSubmit } = useForm();
        const onSubmit = (data) => {
            data.QuoteID = props.userQuoteData.quoteID;
            data.UserCorrectAnswer = data.UserCorrectAnswer === "true" ? true : false;
            props.onSubmitClick(data)
        };
        return (
            <Card style={{ width: '18rem' }}>
                <Card.Body>
                    <Card.Title className="mb-4 text-muted">Who Said It?</Card.Title>
                    <Card.Text className="mb-1">
                        {props.userQuoteData.quoteText}
                    </Card.Text>

                    <Form onSubmit={handleSubmit(onSubmit)}>
                        <Form.Group className="mb-3">
                            <Form.Check type="radio"  {...register("UserCorrectAnswer", { required: true })} value="true" label="Yes" />
                        </Form.Group>

                        <Form.Group className="mb-3">
                            <Form.Check type="radio" {...register("UserCorrectAnswer", { required: true })} value="false" label="No" />
                        </Form.Group>
                        <Button variant="success" type="submit">
                            Submit
                        </Button>
                    </Form>
                </Card.Body>
            </Card>

        );
    }

    static UserMultipleQuoteCard = (props) => {
        const { register, handleSubmit } = useForm();
        const onSubmit = (data) => {
            data.QuoteID = props.userQuoteData.quoteID;
            data.UserMultipleAnswerID = parseInt(data.UserMultipleAnswerID);
            props.onSubmitClick(data)
        };
        return (
            <Card style={{ width: '18rem' }}>
                <Card.Body>
                    <Card.Text className="mb-4 text-muted">Who Said It?</Card.Text>
                    <Card.Title className="mb-1 ">
                        {props.userQuoteData.quoteText}
                    </Card.Title>

                    <Form onSubmit={handleSubmit(onSubmit)}>
                        {props.userQuoteData.quoteMultiplePossibleAnswers.map(possibleAnswer =>
                            <Form.Group className="mb-3" key={possibleAnswer.possibleAnswerID}>
                                <Form.Check type="radio"  {...register("UserMultipleAnswerID", { required: true })}
                                    value={possibleAnswer.possibleAnswerID} label={possibleAnswer.possibleAnwerText} />
                            </Form.Group>
                        )}
                        <Button variant="success" type="submit">
                            Submit
                        </Button>
                    </Form>
                </Card.Body>
            </Card>

        );
    }


    async getUsersData() {
        const response = await fetch('api/UserManagment/GetUsers',
            {
                method: "GET",
                headers: { 'Content-Type': 'application/json' },
            }
        );
        const resultData = await response.json();
        if (resultData) {
            this.setState({ usersData: resultData, loading: false });
        } else {
            this.setState({ showNoQuotes: true, loading: false });
        }
    }

    async getUserQuote(data) {
        this.setState({ loading: true });

        const response = await fetch('api/UserQuote/GetUserQuote?userID=' + data.userID,
            {
                method: "GET",
                headers: { 'Content-Type': 'application/json' },
            }
        );
        const resultData = await response.json();

        if (resultData) {
            this.setState({ userQuoteData: resultData, loading: false, showAnswer: false });
        } else {
            this.setState({ showNoQuotes: true, loading: false, showAnswer: false });
        }
    }

    async answerUserQuote(data) {
        data.userID = 1;

        this.setState({ loading: true });
        const response = await fetch('api/UserQuote/AnswerUserQuote',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            }
        );
        const resultData = await response.json();

        if (resultData) {
            this.setState({ answerResult: resultData, loading: false, showAnswer: true });
        }

    }

}
