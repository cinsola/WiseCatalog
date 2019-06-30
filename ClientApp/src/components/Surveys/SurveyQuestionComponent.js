import React from 'react';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import Typography from '@material-ui/core/Typography';
export class SurveyQuestion extends React.Component {
    constructor(props) {
        super(props);
    }
    render() {
        const question = this.props.question;
        if (question !== null) {
            return (
                <Card>
                    <CardContent>
                        <Typography color="textSecondary" gutterBottom>Domanda</Typography>
                        <Typography variant="h5" component="h2">{question.name}</Typography>
                    </CardContent>
                </Card>
            );
        } else {
            return <div />;
        }
    }
}
