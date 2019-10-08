import React from 'react';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
import EditText from 'react-editext';
import  SpinnerOrNot from '../Common/SpinnerOrNot';
export default class QuestionComponent extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            isEditMode: false
        };
    }

    toggleEditMode() {
        this.setState({ isEditMode: !this.state.isEditMode });
    }

    sendMutation(value) {
        this.props.sendMutation(value);
    }

    render() {
        if (this.props.id !== null) {
            return (
                <Card>
                    <CardContent>
                        <Typography color="textSecondary" gutterBottom>Question</Typography>
                        <SpinnerOrNot isSpinning={this.props.isLoadingOnContext}>
                            {this.state.isEditMode === false &&
                                <Typography variant="h5" component="h2" onClick={() => this.toggleEditMode()}>{this.props.name}</Typography>
                            }
                            {this.state.isEditMode === true &&
                                <EditText type="text" value={this.props.name} onCancel={() => this.toggleEditMode()} onSave={(value) => this.sendMutation(value)} />
                            }
                        </SpinnerOrNot>
                    </CardContent>
                </Card>
            );
        } else {
            return <div />;
        }
    }
}