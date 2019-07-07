import React from 'react';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import EditText from 'react-editext'

export class SurveyQuestion extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            isEditMode: false
        }
    }

    toggleEditMode() {
        this.setState({ isEditMode: !this.state.isEditMode })
    }

    sendMutation(value) {
        this.props.sendMutation({ name: value });
    }

    render() {
        const question = this.props.question;
        if (question !== null) {
            return (
                <Card>
                    <CardContent>
                        <Typography color="textSecondary" gutterBottom>Question</Typography>
                        {this.state.isEditMode === false &&
                            <Typography variant="h5" component="h2" onClick={() => this.toggleEditMode()}>{question.name}</Typography>
                        }
                        {this.state.isEditMode === true &&
                            <EditText type="text" value={question.name} onCancel={() => this.toggleEditMode()} onSave={(value) => this.sendMutation(value)} />
                        }
                    </CardContent>
                </Card>
            );
        } else {
            return <div />;
        }
    }
}
